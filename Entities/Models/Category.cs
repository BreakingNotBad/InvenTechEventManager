using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Category
    {
        public  int CategoryId { get; set; }
        
        public required string CategoryName { get; set; }

        public ICollection<Equipment>? Equipments { get; set; } = new List<Equipment>();
    }
}
