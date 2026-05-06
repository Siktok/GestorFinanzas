using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Controllers;

public class InversionesController(FinanzaNovaDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var inversiones = await context.Inversiones
            .AsNoTracking()
            .Include(i => i.CuentaReferencia)
            .OrderByDescending(i => i.Activa)
            .ThenBy(i => i.Nombre)
            .ToListAsync();

        return View(inversiones);
    }

    public async Task<IActionResult> Create()
    {
        await CargarCuentasAsync();
        return View(new Inversion());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Inversion inversion)
    {
        await ValidarInversionAsync(inversion);

        if (!ModelState.IsValid)
        {
            await CargarCuentasAsync(inversion.CuentaReferenciaId);
            return View(inversion);
        }

        context.Add(inversion);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var inversion = await context.Inversiones.FindAsync(id);
        if (inversion is null)
        {
            return NotFound();
        }

        await CargarCuentasAsync(inversion.CuentaReferenciaId);
        return View(inversion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Inversion inversion)
    {
        if (id != inversion.Id)
        {
            return NotFound();
        }

        await ValidarInversionAsync(inversion);

        if (!ModelState.IsValid)
        {
            await CargarCuentasAsync(inversion.CuentaReferenciaId);
            return View(inversion);
        }

        context.Update(inversion);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActiva(int id)
    {
        var inversion = await context.Inversiones.FindAsync(id);
        if (inversion is null)
        {
            return NotFound();
        }

        inversion.Activa = !inversion.Activa;
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task ValidarInversionAsync(Inversion inversion)
    {
        if (inversion.Activa && !inversion.CuentaReferenciaId.HasValue)
        {
            ModelState.AddModelError(nameof(Inversion.CuentaReferenciaId), "Selecciona la cuenta que financia la inversion.");
            return;
        }

        if (!inversion.CuentaReferenciaId.HasValue)
        {
            return;
        }

        var cuentaActiva = await context.Cuentas.AnyAsync(c => c.Id == inversion.CuentaReferenciaId && c.Activa);
        if (!cuentaActiva)
        {
            ModelState.AddModelError(nameof(Inversion.CuentaReferenciaId), "Selecciona una cuenta activa.");
        }
    }

    private async Task CargarCuentasAsync(int? selectedId = null)
    {
        var cuentas = await context.Cuentas
            .AsNoTracking()
            .Where(c => c.Activa)
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        ViewBag.CuentaReferenciaId = new SelectList(cuentas, nameof(Cuenta.Id), nameof(Cuenta.Nombre), selectedId);
    }
}
