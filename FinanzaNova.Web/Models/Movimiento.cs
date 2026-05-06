using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Models;

public class Movimiento
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Cuenta")]
    public int CuentaId { get; set; }

    public Cuenta? Cuenta { get; set; }

    [Display(Name = "Cuenta destino")]
    public int? CuentaDestinoId { get; set; }

    public Cuenta? CuentaDestino { get; set; }

    [Display(Name = "Categoria")]
    public int? CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }

    [Display(Name = "Tipo")]
    public MovimientoTipo Tipo { get; set; }

    [Display(Name = "Estado")]
    public MovimientoEstado Estado { get; set; } = MovimientoEstado.Confirmado;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de movimiento")]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Introduce una descripcion.")]
    [StringLength(140, ErrorMessage = "La descripcion no puede superar 140 caracteres.")]
    [Display(Name = "Descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [Precision(18, 2)]
    [Range(0.01, 999999999999.99, ErrorMessage = "Introduce un importe valido.")]
    [Display(Name = "Importe")]
    public decimal Importe { get; set; }

    [Display(Name = "Fecha de creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}
