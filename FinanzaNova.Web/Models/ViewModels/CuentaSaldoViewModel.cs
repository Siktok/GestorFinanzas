namespace FinanzaNova.Web.Models.ViewModels;

public class CuentaSaldoViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public bool Activa { get; set; }
}
