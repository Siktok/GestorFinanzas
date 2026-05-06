using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanzaNova.Web.Models.ViewModels;

public class MovimientosIndexViewModel
{
    public int? CuentaId { get; set; }
    public string? Tipo { get; set; }
    public List<SelectListItem> Cuentas { get; set; } = [];
    public List<SelectListItem> Tipos { get; set; } = [];
    public List<MovimientoListadoViewModel> Movimientos { get; set; } = [];
}
