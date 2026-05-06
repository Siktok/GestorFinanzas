using System.Net;

namespace FinanzaNova.Tests;

public class RoutingTests(FinanzaNovaWebApplicationFactory factory) : IClassFixture<FinanzaNovaWebApplicationFactory>
{
    [Theory]
    [InlineData("/")]
    [InlineData("/Cuentas")]
    [InlineData("/Movimientos")]
    [InlineData("/Categorias")]
    [InlineData("/Inversiones")]
    public async Task Get_RutasPrincipales_DevuelveRespuestaExitosa(string url)
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
