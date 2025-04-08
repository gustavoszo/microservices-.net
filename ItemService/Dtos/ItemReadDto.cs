namespace ItemService.Dtos
{
    public class ItemReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int IdRestaurant { get; set; }
    }
}