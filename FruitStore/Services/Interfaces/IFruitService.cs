using FruitStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitStore.Services.Interfaces
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitItemDTO>> GetFruitItems();

        Task<FruitItemDTO> AddFruitItem(FruitItemDTO fruit);

        Task<FruitItemDTO> DeleteFruitItem(int id);

        Task<FruitItemDTO> GetFruitItemById(int id);
    }
}
