using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System.Threading.Tasks;

namespace AspNetCoreIntegrationTestSample.IntegrationTests.Suporte
{
    public class TesteBaseBanco
    {
        private static readonly Checkpoint _apagadorDeDados = new Checkpoint
        {
            TablesToIgnore = new[] {"__EFMigrationsHistory"}
        };

        protected static IServiceScope _scope;
        protected static IServiceScope _scopeParaAsserts;
        protected static T GetService<T>() => _scope.ServiceProvider.GetService<T>();
        protected static T GetServiceParaAsserts<T>() => _scopeParaAsserts.ServiceProvider.GetService<T>();

        // Simula scope do request
        [SetUp]
        public async Task SetUpScope()
        {
            _scope = AmbienteTestes.Factory.Services.CreateScope();
            _scopeParaAsserts = AmbienteTestes.Factory.Services.CreateScope();

            var configuration =
                (ConfigurationRoot) AmbienteTestes.Factory.Services.GetService(typeof(IConfiguration));
            var connectionString = configuration.GetConnectionString("BloggingDatabase");

            await _apagadorDeDados.Reset(connectionString);
        }

        // Dispose do scope do request
        [TearDown]
        public void TearDownScope()
        {
            _scope.Dispose();
            _scopeParaAsserts.Dispose();
        }
    }
}