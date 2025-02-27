namespace PawFect.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        // Kategori ilişkisi için CategoryId ve Category özellikleri
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
        public Product()
        {
            Comments = new List<Comment>();
        }

    }
}
