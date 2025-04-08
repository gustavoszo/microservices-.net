using System.ComponentModel.DataAnnotations;

namespace ItemService.Dtos
{
    public class ItemCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}