using FruitStore.Models;
using FruitStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitItemsController : ControllerBase
    {
        private readonly IFruitService _fruitService;

        public FruitItemsController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitItemDTO>>> Get()
        {
            var fruits = await _fruitService.GetFruitItems();
            return Ok(fruits);
        }

        [HttpPost]
        public async Task<ActionResult<FruitItemDTO>> Post(FruitItemDTO fruitItem)
        {
            var savedFruit = await _fruitService.AddFruitItem(fruitItem);
            return StatusCode(201, savedFruit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var FruitItem = await _fruitService.DeleteFruitItem(id);

            if (FruitItem == null)
            {
                string message = string.Format("No Product found with ID = {0}", id);
                return StatusCode(404, message);
            }
            return StatusCode(204, FruitItem); ;
        }
    }
}