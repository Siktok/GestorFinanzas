using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using FinanzaNova.Web.Models.ViewModels;
using FinanzaNova.Web.Services;

namespace FinanzaNova.Web.Controllers;

public class HomeController(FinanzaNovaDbContext context, FinanzasResumenService resumenService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var saldos = await resumenService.ObtenerSaldosCuentasAsync();
        var cuentasActivas = await context.Cuentas
            .AsNoTracking()
            .Where(c => c.Activa)
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        var inversiones = await context.Inversiones
            .AsNoTracking()
            .Where(i => i.Activa)
            .OrderByDescending(i => i.ValorActual)
            .ToListAsync();

        var desde = DateTime.Today.AddMonths(-5);
        var movimientosConfirmados = await context.Movimientos
            .AsNoTracking()
            .Include(m => m.Cuenta)
            .Include(m => m.CuentaDestino)
            .Include(m => m.Categoria)
            .Where(m => m.Estado == MovimientoEstado.Confirmado)
            .OrderByDescending(m => m.Fecha)
            .ThenByDescending(m => m.Id)
            .ToListAsync();

        var inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var movimientosMes = movimientosConfirmados.Where(m => m.Fecha >= inicioMes).ToList();
        var ingresosMes = movimientosMes.Where(m => m.Tipo == MovimientoTipo.Ingreso).Sum(m => m.Importe);
        var gastosMes = movimientosMes.Where(m => m.Tipo == MovimientoTipo.Gasto).Sum(m => m.Importe);

        var evolucion = Enumerable.Range(0, 6)
            .Select(offset => new DateTime(desde.Year, desde.Month, 1).AddMonths(offset))
            .Select(mes => new GraficoValorViewModel
            {
                Etiqueta = mes.ToString("MMM yy"),
                Valor = movimientosConfirmados
                    .Where(m => m.Fecha.Year == mes.Year && m.Fecha.Month == mes.Month)
                    .Sum(m => m.Tipo == MovimientoTipo.Ingreso ? m.Importe : m.Tipo == MovimientoTipo.Gasto ? -m.Importe : 0)
            })
            .ToList();

        var gastosPorCategoria = movimientosMes
            .Where(m => m.Tipo == MovimientoTipo.Gasto)
            .GroupBy(m => m.Categoria?.Nombre ?? "Sin categoria")
            .Select(g => new GraficoValorViewModel { Etiqueta = g.Key, Valor = g.Sum(m => m.Importe) })
            .OrderByDescending(g => g.Valor)
            .Take(6)
            .ToList();

        var model = new DashboardViewModel
        {
            LiquidezTotal = cuentasActivas.Sum(c => saldos.GetValueOrDefault(c.Id)),
            InversionesTotal = inversiones.Sum(i => i.ValorActual),
            IngresosMes = ingresosMes,
            GastosMes = gastosMes,
            Cuentas = cuentasActivas.Select(c => new CuentaSaldoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Tipo = c.Tipo.ToString(),
                Saldo = saldos.GetValueOrDefault(c.Id),
                Activa = c.Activa
            }).ToList(),
            Inversiones = inversiones.Select(i => new InversionResumenViewModel
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Tipo = i.Tipo.ToString(),
                ImporteInvertido = i.ImporteInvertido,
                ValorActual = i.ValorActual
            }).ToList(),
            UltimosMovimientos = movimientosConfirmados.Take(8).Select(m => new MovimientoListadoViewModel
            {
                Id = m.Id,
                Fecha = m.Fecha,
                Descripcion = m.Descripcion,
                Cuenta = m.Cuenta?.Nombre ?? string.Empty,
                CuentaDestino = m.CuentaDestino?.Nombre,
                Categoria = m.Categoria?.Nombre,
                Tipo = m.Tipo.ToString(),
                Estado = m.Estado.ToString(),
                Importe = m.Importe
            }).ToList(),
            EvolucionMensual = evolucion,
            GastosPorCategoria = gastosPorCategoria
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
