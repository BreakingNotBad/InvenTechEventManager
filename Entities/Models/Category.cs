namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }

        // Navigation Property
        public ICollection<Equipment>? Equipments { get; set; } = [];
    }
}
