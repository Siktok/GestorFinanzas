using FinanzaNova.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Data;

public class FinanzaNovaDbContext(DbContextOptions<FinanzaNovaDbContext> options) : DbContext(options)
{
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Movimiento> Movimientos => Set<Movimiento>();
    public DbSet<Inversion> Inversiones => Set<Inversion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cuenta>()
            .Property(c => c.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Categoria>()
            .Property(c => c.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Movimiento>()
            .Property(m => m.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Movimiento>()
            .Property(m => m.Estado)
            .HasConversion<string>();

        modelBuilder.Entity<Inversion>()
            .Property(i => i.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Cuenta)
            .WithMany(c => c.Movimientos)
            .HasForeignKey(m => m.CuentaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.CuentaDestino)
            .WithMany(c => c.TransferenciasEntrantes)
            .HasForeignKey(m => m.CuentaDestinoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Categoria)
            .WithMany(c => c.Movimientos)
            .HasForeignKey(m => m.CategoriaId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Inversion>()
            .HasOne(i => i.CuentaReferencia)
            .WithMany(c => c.Inversiones)
            .HasForeignKey(i => i.CuentaReferenciaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
