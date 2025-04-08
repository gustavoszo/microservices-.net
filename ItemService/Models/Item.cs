using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int IdRestaurant { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}