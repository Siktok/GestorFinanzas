using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using FinanzaNova.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Controllers;

public class MovimientosController(FinanzaNovaDbContext context) : Controller
{
    public async Task<IActionResult> Index(int? cuentaId, string? tipo)
    {
        var query = context.Movimientos
            .AsNoTracking()
            .Include(m => m.Cuenta)
            .Include(m => m.CuentaDestino)
            .Include(m => m.Categoria)
            .AsQueryable();

        if (cuentaId.HasValue)
        {
            query = query.Where(m => m.CuentaId == cuentaId || m.CuentaDestinoId == cuentaId);
        }

        if (Enum.TryParse<MovimientoTipo>(tipo, out var tipoFiltro))
        {
            query = query.Where(m => m.Tipo == tipoFiltro);
        }

        var movimientos = await query
            .OrderByDescending(m => m.Fecha)
            .ThenByDescending(m => m.Id)
            .Select(m => new MovimientoListadoViewModel
            {
                Id = m.Id,
                Fecha = m.Fecha,
                Descripcion = m.Descripcion,
                Cuenta = m.Cuenta == null ? string.Empty : m.Cuenta.Nombre,
                CuentaDestino = m.CuentaDestino == null ? null : m.CuentaDestino.Nombre,
                Categoria = m.Categoria == null ? null : m.Categoria.Nombre,
                Tipo = m.Tipo.ToString(),
                Estado = m.Estado.ToString(),
                Importe = m.Importe
            })
            .ToListAsync();

        return View(new MovimientosIndexViewModel
        {
            CuentaId = cuentaId,
            Tipo = tipo,
            Cuentas = await ObtenerCuentasSelectListAsync(cuentaId),
            Tipos = ObtenerTiposSelectList(tipo),
            Movimientos = movimientos
        });
    }

    public async Task<IActionResult> Create()
    {
        await CargarFormularioAsync();
        return View(new Movimiento());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movimiento movimiento)
    {
        await ValidarMovimientoAsync(movimiento);

        if (!ModelState.IsValid)
        {
            await CargarFormularioAsync(movimiento);
            return View(movimiento);
        }

        NormalizarMovimiento(movimiento);
        context.Add(movimiento);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var movimiento = await context.Movimientos.FindAsync(id);
        if (movimiento is null)
        {
            return NotFound();
        }

        await CargarFormularioAsync(movimiento);
        return View(movimiento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movimiento movimiento)
    {
        if (id != movimiento.Id)
        {
            return NotFound();
        }

        await ValidarMovimientoAsync(movimiento);

        if (!ModelState.IsValid)
        {
            await CargarFormularioAsync(movimiento);
            return View(movimiento);
        }

        NormalizarMovimiento(movimiento);
        context.Update(movimiento);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var movimiento = await context.Movimientos.FindAsync(id);
        if (movimiento is null)
        {
            return NotFound();
        }

        context.Remove(movimiento);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task ValidarMovimientoAsync(Movimiento movimiento)
    {
        var cuentaExiste = await context.Cuentas.AnyAsync(c => c.Id == movimiento.CuentaId && c.Activa);
        if (!cuentaExiste)
        {
            ModelState.AddModelError(nameof(Movimiento.CuentaId), "Selecciona una cuenta activa.");
        }

        if (movimiento.Tipo == MovimientoTipo.Transferencia)
        {
            if (!movimiento.CuentaDestinoId.HasValue)
            {
                ModelState.AddModelError(nameof(Movimiento.CuentaDestinoId), "Selecciona una cuenta destino.");
            }
            else if (movimiento.CuentaDestinoId == movimiento.CuentaId)
            {
                ModelState.AddModelError(nameof(Movimiento.CuentaDestinoId), "La cuenta destino debe ser distinta.");
            }
            else
            {
                var destinoExiste = await context.Cuentas.AnyAsync(c => c.Id == movimiento.CuentaDestinoId && c.Activa);
                if (!destinoExiste)
                {
                    ModelState.AddModelError(nameof(Movimiento.CuentaDestinoId), "Selecciona una cuenta destino activa.");
                }
            }
        }
        else if (!movimiento.CategoriaId.HasValue)
        {
            ModelState.AddModelError(nameof(Movimiento.CategoriaId), "Selecciona una categoria.");
        }
        else
        {
            var tipoCategoria = movimiento.Tipo == MovimientoTipo.Ingreso ? CategoriaTipo.Ingreso : CategoriaTipo.Gasto;
            var categoriaValida = await context.Categorias.AnyAsync(c =>
                c.Id == movimiento.CategoriaId &&
                c.Activa &&
                c.Tipo == tipoCategoria);

            if (!categoriaValida)
            {
                ModelState.AddModelError(nameof(Movimiento.CategoriaId), "Selecciona una categoria compatible con el tipo de movimiento.");
            }
        }
    }

    private static void NormalizarMovimiento(Movimiento movimiento)
    {
        if (movimiento.Tipo == MovimientoTipo.Transferencia)
        {
            movimiento.CategoriaId = null;
        }
        else
        {
            movimiento.CuentaDestinoId = null;
        }
    }

    private async Task CargarFormularioAsync(Movimiento? movimiento = null)
    {
        var selectedCuentaId = movimiento?.CuentaId;
        var selectedDestinoId = movimiento?.CuentaDestinoId;
        var selectedCategoriaId = movimiento?.CategoriaId;

        ViewBag.CuentaId = new SelectList(await ObtenerCuentasActivasAsync(), nameof(Cuenta.Id), nameof(Cuenta.Nombre), selectedCuentaId);
        ViewBag.CuentaDestinoId = new SelectList(await ObtenerCuentasActivasAsync(), nameof(Cuenta.Id), nameof(Cuenta.Nombre), selectedDestinoId);
        ViewBag.CategoriaId = new SelectList(await ObtenerCategoriasActivasAsync(), nameof(Categoria.Id), nameof(Categoria.Nombre), selectedCategoriaId);
    }

    private async Task<List<Cuenta>> ObtenerCuentasActivasAsync()
    {
        return await context.Cuentas
            .AsNoTracking()
            .Where(c => c.Activa)
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }

    private async Task<List<Categoria>> ObtenerCategoriasActivasAsync()
    {
        return await context.Categorias
            .AsNoTracking()
            .Where(c => c.Activa)
            .OrderBy(c => c.Tipo)
            .ThenBy(c => c.Nombre)
            .ToListAsync();
    }

    private async Task<List<SelectListItem>> ObtenerCuentasSelectListAsync(int? selectedId)
    {
        var cuentas = await ObtenerCuentasActivasAsync();
        return cuentas.Select(c => new SelectListItem(c.Nombre, c.Id.ToString(), c.Id == selectedId)).ToList();
    }

    private static List<SelectListItem> ObtenerTiposSelectList(string? selected)
    {
        return Enum.GetNames<MovimientoTipo>()
            .Select(t => new SelectListItem(t, t, t == selected))
            .ToList();
    }
}
