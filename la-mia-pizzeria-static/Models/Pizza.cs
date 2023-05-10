using la_mia_pizzeria_static.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("pizza")]
    public class Pizza
    {
        [Key] public int Id { get; set; }
        [Required] [StringLength(15,MinimumLength = 5)] public string Name { get; set; }
        [Required][StringLength(45, MinimumLength = 10)] [FiveWords] public string Description { get; set; }
        [Required] [Range(1,100)] public double Price { get; set; }
        [Required] [Url] public string Image { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
    }
}
