using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Restaurant
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdExternal { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Item> Itens { get; set; } = new List<Item>();
    }
}