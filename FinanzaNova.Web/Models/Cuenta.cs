using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Models;

public class Cuenta
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Introduce el nombre de la cuenta.")]
    [StringLength(80, ErrorMessage = "El nombre no puede superar 80 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Tipo")]
    public CuentaTipo Tipo { get; set; }

    [Precision(18, 2)]
    [Display(Name = "Saldo inicial")]
    public decimal SaldoInicial { get; set; }

    [Display(Name = "Activa")]
    public bool Activa { get; set; } = true;

    [Display(Name = "Fecha de creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Today;

    public ICollection<Movimiento> Movimientos { get; set; } = [];
    public ICollection<Movimiento> TransferenciasEntrantes { get; set; } = [];
    public ICollection<Inversion> Inversiones { get; set; } = [];
}
