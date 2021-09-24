using FruitStore.Database;
using FruitStore.Models;
using FruitStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitStore.Services.Classes
{
    public class FruitService : IFruitService
    {
        private readonly FruitDbContext _context;

        public FruitService(FruitDbContext fruitContext)
        {
            _context = fruitContext;
        }

        public async Task<FruitItemDTO> AddFruitItem(FruitItemDTO fruit)
        {
            Log.Information("Adding the fruit to the fruit store, {@fruit}", fruit);
            _context.FruitItems.Add(fruit);
            await _context.SaveChangesAsync();
            return fruit; ;
        }

        public async Task<FruitItemDTO> DeleteFruitItem(int id)
        {
            var fruitItem = await GetFruitItemById(id);
            if (fruitItem == null)
            {
                Log.Error("Unable to find fruit in the fruit store");
                return null;
            }
            Log.Information("Found the fruit in the fruit store, delete it");
            _context.FruitItems.Remove(fruitItem);
            await _context.SaveChangesAsync();
            return fruitItem;
        }

        public async Task<IEnumerable<FruitItemDTO>> GetFruitItems()
        {
            Log.Information("Get all the fruit items from the fruit store");
            return await _context.FruitItems.ToListAsync();
        }

        public async Task<FruitItemDTO> GetFruitItemById(int id)
        {
            Log.Information("Get a fruit item with id from the fruit store");
            return await _context.FruitItems.FirstOrDefaultAsync(o => o.FruitId == id);
        }
    }
}
