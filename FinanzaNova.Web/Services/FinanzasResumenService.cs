using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Services;

public class FinanzasResumenService(FinanzaNovaDbContext context)
{
    public async Task<Dictionary<int, decimal>> ObtenerSaldosCuentasAsync()
    {
        var cuentas = await context.Cuentas.AsNoTracking().ToListAsync();
        var saldos = cuentas.ToDictionary(c => c.Id, c => c.SaldoInicial);

        var movimientos = await context.Movimientos
            .AsNoTracking()
            .Where(m => m.Estado == MovimientoEstado.Confirmado)
            .ToListAsync();

        foreach (var movimiento in movimientos)
        {
            if (!saldos.ContainsKey(movimiento.CuentaId))
            {
                continue;
            }

            if (movimiento.Tipo == MovimientoTipo.Ingreso)
            {
                saldos[movimiento.CuentaId] += movimiento.Importe;
            }
            else if (movimiento.Tipo == MovimientoTipo.Gasto)
            {
                saldos[movimiento.CuentaId] -= movimiento.Importe;
            }
            else if (movimiento.Tipo == MovimientoTipo.Transferencia)
            {
                saldos[movimiento.CuentaId] -= movimiento.Importe;

                if (movimiento.CuentaDestinoId.HasValue && saldos.ContainsKey(movimiento.CuentaDestinoId.Value))
                {
                    saldos[movimiento.CuentaDestinoId.Value] += movimiento.Importe;
                }
            }
        }

        var inversionesActivas = await context.Inversiones
            .AsNoTracking()
            .Where(i => i.Activa && i.CuentaReferenciaId.HasValue)
            .ToListAsync();

        foreach (var inversion in inversionesActivas)
        {
            if (inversion.CuentaReferenciaId.HasValue && saldos.ContainsKey(inversion.CuentaReferenciaId.Value))
            {
                saldos[inversion.CuentaReferenciaId.Value] -= inversion.ImporteInvertido;
            }
        }

        return saldos;
    }
}
