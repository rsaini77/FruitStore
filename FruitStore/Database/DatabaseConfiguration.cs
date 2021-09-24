using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FruitStore.Database
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection service, string databaseName)
        {
            service.AddDbContext<FruitDbContext>(o=>o.UseInMemoryDatabase(databaseName));
        }
    }
}
