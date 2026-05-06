namespace FinanzaNova.Web.Models.ViewModels;

public class DashboardViewModel
{
    public decimal LiquidezTotal { get; set; }
    public decimal InversionesTotal { get; set; }
    public decimal PatrimonioTotal => LiquidezTotal + InversionesTotal;
    public decimal IngresosMes { get; set; }
    public decimal GastosMes { get; set; }
    public List<CuentaSaldoViewModel> Cuentas { get; set; } = [];
    public List<InversionResumenViewModel> Inversiones { get; set; } = [];
    public List<MovimientoListadoViewModel> UltimosMovimientos { get; set; } = [];
    public List<GraficoValorViewModel> EvolucionMensual { get; set; } = [];
    public List<GraficoValorViewModel> GastosPorCategoria { get; set; } = [];
}
