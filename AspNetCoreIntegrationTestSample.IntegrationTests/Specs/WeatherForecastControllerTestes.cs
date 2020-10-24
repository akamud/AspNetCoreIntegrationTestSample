using AspNetCoreIntegrationTestSample.IntegrationTests.Suporte;
using FluentAssertions;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspNetCoreIntegrationTestSample.IntegrationTests.Specs
{
    public class WeatherForecastControllerTestes : TesteBaseApi
    {
        [Test]
        public async Task GetDeveRetornarArrayComPrevisoes()
        {
            var resultado = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("weatherForecast");

            resultado.Should().HaveCount(5);
        }
    }
}