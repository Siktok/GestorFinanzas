using FinanzaNova.Web.Data;
using FinanzaNova.Web.Models;
using FinanzaNova.Web.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Tests;

public class FinanzasResumenServiceTests
{
    [Fact]
    public async Task ObtenerSaldosCuentasAsync_DevuelveSaldoInicialSinMovimientos()
    {
        await using var database = await TestDatabase.CreateAsync();
        database.Context.Cuentas.Add(new Cuenta
        {
            Nombre = "Cuenta principal",
            Tipo = CuentaTipo.Banco,
            SaldoInicial = 1000m
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(1000m, saldos.Single().Value);
    }

    [Fact]
    public async Task ObtenerSaldosCuentasAsync_SumaIngresosConfirmados()
    {
        await using var database = await TestDatabase.CreateAsync();
        var cuenta = await CrearCuentaAsync(database.Context, 1000m);
        database.Context.Movimientos.Add(new Movimiento
        {
            CuentaId = cuenta.Id,
            Tipo = MovimientoTipo.Ingreso,
            Estado = MovimientoEstado.Confirmado,
            Descripcion = "Nomina",
            Importe = 250m
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(1250m, saldos[cuenta.Id]);
    }

    [Fact]
    public async Task ObtenerSaldosCuentasAsync_RestaGastosConfirmados()
    {
        await using var database = await TestDatabase.CreateAsync();
        var cuenta = await CrearCuentaAsync(database.Context, 1000m);
        database.Context.Movimientos.Add(new Movimiento
        {
            CuentaId = cuenta.Id,
            Tipo = MovimientoTipo.Gasto,
            Estado = MovimientoEstado.Confirmado,
            Descripcion = "Alquiler",
            Importe = 300m
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(700m, saldos[cuenta.Id]);
    }

    [Fact]
    public async Task ObtenerSaldosCuentasAsync_IgnoraMovimientosPendientes()
    {
        await using var database = await TestDatabase.CreateAsync();
        var cuenta = await CrearCuentaAsync(database.Context, 1000m);
        database.Context.Movimientos.Add(new Movimiento
        {
            CuentaId = cuenta.Id,
            Tipo = MovimientoTipo.Gasto,
            Estado = MovimientoEstado.Pendiente,
            Descripcion = "Recibo pendiente",
            Importe = 200m
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(1000m, saldos[cuenta.Id]);
    }

    [Fact]
    public async Task ObtenerSaldosCuentasAsync_TransfiereEntreCuentas()
    {
        await using var database = await TestDatabase.CreateAsync();
        var origen = await CrearCuentaAsync(database.Context, 1000m, "Origen");
        var destino = await CrearCuentaAsync(database.Context, 100m, "Destino");
        database.Context.Movimientos.Add(new Movimiento
        {
            CuentaId = origen.Id,
            CuentaDestinoId = destino.Id,
            Tipo = MovimientoTipo.Transferencia,
            Estado = MovimientoEstado.Confirmado,
            Descripcion = "Traspaso",
            Importe = 300m
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(700m, saldos[origen.Id]);
        Assert.Equal(400m, saldos[destino.Id]);
    }

    [Fact]
    public async Task ObtenerSaldosCuentasAsync_RestaInversionesActivas()
    {
        await using var database = await TestDatabase.CreateAsync();
        var cuenta = await CrearCuentaAsync(database.Context, 1000m);
        database.Context.Inversiones.Add(new Inversion
        {
            CuentaReferenciaId = cuenta.Id,
            Nombre = "Fondo indexado",
            Tipo = InversionTipo.Fondo,
            ImporteInvertido = 350m,
            ValorActual = 380m,
            Activa = true
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(650m, saldos[cuenta.Id]);
    }

    [Fact]
    public async Task ObtenerSaldosCuentasAsync_NoRestaInversionesArchivadas()
    {
        await using var database = await TestDatabase.CreateAsync();
        var cuenta = await CrearCuentaAsync(database.Context, 1000m);
        database.Context.Inversiones.Add(new Inversion
        {
            CuentaReferenciaId = cuenta.Id,
            Nombre = "Deposito cerrado",
            Tipo = InversionTipo.Deposito,
            ImporteInvertido = 350m,
            ValorActual = 360m,
            Activa = false
        });
        await database.Context.SaveChangesAsync();

        var saldos = await new FinanzasResumenService(database.Context).ObtenerSaldosCuentasAsync();

        Assert.Equal(1000m, saldos[cuenta.Id]);
    }

    private static async Task<Cuenta> CrearCuentaAsync(
        FinanzaNovaDbContext context,
        decimal saldoInicial,
        string nombre = "Cuenta")
    {
        var cuenta = new Cuenta
        {
            Nombre = nombre,
            Tipo = CuentaTipo.Banco,
            SaldoInicial = saldoInicial
        };

        context.Cuentas.Add(cuenta);
        await context.SaveChangesAsync();
        return cuenta;
    }

    private sealed class TestDatabase : IAsyncDisposable
    {
        private readonly SqliteConnection connection;

        private TestDatabase(SqliteConnection connection, FinanzaNovaDbContext context)
        {
            this.connection = connection;
            Context = context;
        }

        public FinanzaNovaDbContext Context { get; }

        public static async Task<TestDatabase> CreateAsync()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<FinanzaNovaDbContext>()
                .UseSqlite(connection)
                .Options;
            var context = new FinanzaNovaDbContext(options);
            await context.Database.EnsureCreatedAsync();

            return new TestDatabase(connection, context);
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
            await connection.DisposeAsync();
        }
    }
}
