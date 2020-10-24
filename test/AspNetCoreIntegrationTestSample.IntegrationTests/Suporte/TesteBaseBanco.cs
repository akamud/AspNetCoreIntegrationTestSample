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
        protected static BloggingContext _contextParaTestes;
        protected static T GetService<T>() => _scope.ServiceProvider.GetService<T>();

        // Simula scope do request
        [SetUp]
        public async Task SetUpScope()
        {
            _scope = AmbienteTestes.Factory.Services.CreateScope();
            _contextParaTestes = AmbienteTestes.Factory.Services.CreateScope().ServiceProvider
                .GetService<BloggingContext>();

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
            _contextParaTestes.Dispose();
        }
    }
}