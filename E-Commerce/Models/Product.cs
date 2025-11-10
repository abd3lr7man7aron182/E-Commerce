using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(3,ErrorMessage ="Lenght is not Valid")]
        public string Name { get; set; }
    }
}
 