using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanzaNova.Web.Models;

public class Inversion
{
    public int Id { get; set; }

    [Display(Name = "Cuenta de financiacion")]
    public int? CuentaReferenciaId { get; set; }

    public Cuenta? CuentaReferencia { get; set; }

    [Required(ErrorMessage = "Introduce el nombre de la inversion.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Tipo")]
    public InversionTipo Tipo { get; set; }

    [Precision(18, 2)]
    [Range(0, 999999999999.99, ErrorMessage = "Introduce un importe invertido valido.")]
    [Display(Name = "Importe invertido")]
    public decimal ImporteInvertido { get; set; }

    [Precision(18, 2)]
    [Range(0, 999999999999.99, ErrorMessage = "Introduce un valor actual valido.")]
    [Display(Name = "Valor actual")]
    public decimal ValorActual { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de valoracion")]
    public DateTime FechaValoracion { get; set; } = DateTime.Today;

    [Display(Name = "Activa")]
    public bool Activa { get; set; } = true;

    [StringLength(500, ErrorMessage = "Las notas no pueden superar 500 caracteres.")]
    [Display(Name = "Notas")]
    public string? Notas { get; set; }
}
