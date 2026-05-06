using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using FinanzaNova.Web.Models.ViewModels;
using FinanzaNova.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Controllers;

public class CuentasController(FinanzaNovaDbContext context, FinanzasResumenService resumenService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var saldos = await resumenService.ObtenerSaldosCuentasAsync();
        var cuentas = await context.Cuentas
            .AsNoTracking()
            .OrderByDescending(c => c.Activa)
            .ThenBy(c => c.Nombre)
            .ToListAsync();

        var model = cuentas.Select(c => new CuentaSaldoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Tipo = c.Tipo.ToString(),
                Saldo = saldos.GetValueOrDefault(c.Id),
                Activa = c.Activa
            })
            .ToList();

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cuenta = await context.Cuentas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cuenta is null)
        {
            return NotFound();
        }

        var saldos = await resumenService.ObtenerSaldosCuentasAsync();
        ViewBag.SaldoActual = saldos.GetValueOrDefault(cuenta.Id);
        var inversiones = await context.Inversiones
            .AsNoTracking()
            .Where(i => i.Activa && i.CuentaReferenciaId == id)
            .OrderByDescending(i => i.ValorActual)
            .ToListAsync();

        ViewBag.CapitalInvertido = inversiones.Sum(i => i.ImporteInvertido);
        ViewBag.Inversiones = inversiones;
        ViewBag.Movimientos = await context.Movimientos
            .AsNoTracking()
            .Include(m => m.Categoria)
            .Include(m => m.CuentaDestino)
            .Where(m => m.CuentaId == id || m.CuentaDestinoId == id)
            .OrderByDescending(m => m.Fecha)
            .ThenByDescending(m => m.Id)
            .Take(20)
            .ToListAsync();

        return View(cuenta);
    }

    public IActionResult Create()
    {
        return View(new Cuenta());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cuenta cuenta)
    {
        if (!ModelState.IsValid)
        {
            return View(cuenta);
        }

        cuenta.FechaCreacion = DateTime.Today;
        context.Add(cuenta);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cuenta = await context.Cuentas.FindAsync(id);
        return cuenta is null ? NotFound() : View(cuenta);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cuenta cuenta)
    {
        if (id != cuenta.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(cuenta);
        }

        context.Update(cuenta);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActiva(int id)
    {
        var cuenta = await context.Cuentas.FindAsync(id);
        if (cuenta is null)
        {
            return NotFound();
        }

        cuenta.Activa = !cuenta.Activa;
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
