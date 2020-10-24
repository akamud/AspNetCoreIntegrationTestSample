using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AspNetCoreIntegrationTestSample.IntegrationTests
{
    [SetUpFixture]
    public class AmbienteTestes
    {
        public static WebApplicationFactory<Startup> Factory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Configura mocks
                });
            });
            
            using var scopeMigration = Factory.Services.CreateScope();
            var bloggingContext = scopeMigration.ServiceProvider.GetService<BloggingContext>();

            bloggingContext.Database.EnsureDeleted();
            bloggingContext.Database.Migrate();
            
            // Adiciona seed inicial
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Factory.Dispose();
        }
    }
}