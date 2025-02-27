using PawFect.Entities;
namespace PawFect.WebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<Product> Products { get; set; }
    }
}
