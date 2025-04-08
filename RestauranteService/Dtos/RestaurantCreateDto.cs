using System.ComponentModel.DataAnnotations;

namespace RestauranteService.Dtos
{
    public class RestaurantCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Site { get; set; }
    }
}