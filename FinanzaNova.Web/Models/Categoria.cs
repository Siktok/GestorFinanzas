using System.ComponentModel.DataAnnotations;

namespace FinanzaNova.Web.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Introduce el nombre de la categoria.")]
    [StringLength(80, ErrorMessage = "El nombre no puede superar 80 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Tipo")]
    public CategoriaTipo Tipo { get; set; }

    [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "Usa un color hexadecimal valido.")]
    [Display(Name = "Color")]
    public string ColorHex { get; set; } = "#0d6efd";

    [Display(Name = "Activa")]
    public bool Activa { get; set; } = true;

    public ICollection<Movimiento> Movimientos { get; set; } = [];
}
