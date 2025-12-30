using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }

        public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    }
}
