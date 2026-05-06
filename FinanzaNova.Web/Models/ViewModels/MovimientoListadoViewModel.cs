namespace FinanzaNova.Web.Models.ViewModels;

public class MovimientoListadoViewModel
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Cuenta { get; set; } = string.Empty;
    public string? CuentaDestino { get; set; }
    public string? Categoria { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal Importe { get; set; }
}
