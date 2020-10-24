using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreIntegrationTestSample.IntegrationTests.Suporte
{
    public class TesteBaseBanco
    {
        protected static IServiceScope _scope;
        protected static T GetService<T>() => _scope.ServiceProvider.GetService<T>();

        // Simula scope do request
        [SetUp]
        public void SetUpScope()
        {
            _scope = AmbienteTestes.Factory.Services.CreateScope();
        }

        // Dispose do scope do request
        [TearDown]
        public void TearDownScope()
        {
            _scope.Dispose();
        }
    }
}