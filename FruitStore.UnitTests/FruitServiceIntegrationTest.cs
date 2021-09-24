using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using System.Threading.Tasks;
using WebAPI;

namespace FruitStore.IntegrationTests
{
    [TestClass]
    public class FruitServiceIntegrationTests
    {
        public FruitServiceIntegrationTests()
        {
        }

        [TestMethod]
        public async Task TestPostFruitItemsAsync()
        {

            // Arrange
            var request = new
            {
                Url = "/api/FruitItems",
                Body = new
                {
                    FruitId = 1,
                    FruitName = "Orange",
                    Quantity = 7,
                    Price = 7,
                }
            };
            var webHostBuilder =
                new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>()
                .UseSerilog();

            // Act
            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();
            var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            await response.Content.ReadAsStringAsync();
            
            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
