using PawFect.Entities;
using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Ürün adı min 3 max 30 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı geçerli bir sayı olmalıdır ve 0'dan küçük olamaz.")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat geçerli bir değer olmalıdır. Lütfen pozitif bir sayı giriniz.")]
        public decimal Price { get; set; }
        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Ürün açıklaması zorunludur.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Ürün açıklaması min 5 max 200 karakter olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
