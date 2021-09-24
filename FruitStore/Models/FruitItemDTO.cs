using System.ComponentModel.DataAnnotations;

namespace FruitStore.Models
{
    public class FruitItemDTO
    {
        [Key]
        public int FruitId { get; set; }
        
        [Required(ErrorMessage = "Fruit name is a required field")]
        public string FruitName { get; set; }

        [Required(ErrorMessage = "Quantity is a required field")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is a required field")]
        public decimal Price { get; set; }
    }
}
