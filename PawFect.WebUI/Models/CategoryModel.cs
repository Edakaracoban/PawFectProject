using PawFect.Entities;
using System.ComponentModel.DataAnnotations;
namespace PawFect.WebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        public List<Product> Products { get; set; }
    }
}
