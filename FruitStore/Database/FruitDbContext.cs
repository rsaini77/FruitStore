using FruitStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FruitStore.Database
{
    public class FruitDbContext : DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options)
            : base(options)
        {
        }
        public DbSet<FruitItemDTO> FruitItems { get; set; }
    }
}
