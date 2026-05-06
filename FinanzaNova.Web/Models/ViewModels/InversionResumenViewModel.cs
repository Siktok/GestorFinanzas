namespace FinanzaNova.Web.Models.ViewModels;

public class InversionResumenViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal ImporteInvertido { get; set; }
    public decimal ValorActual { get; set; }
    public decimal Resultado => ValorActual - ImporteInvertido;
    public decimal Rentabilidad => ImporteInvertido == 0 ? 0 : Resultado / ImporteInvertido * 100;
}
