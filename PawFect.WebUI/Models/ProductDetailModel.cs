using PawFect.Entities;

namespace PawFect.WebUI.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
