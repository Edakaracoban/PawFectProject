using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Entities
{
    public class CartItem //sepetin içindeki elemanlar
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } //navigation property // ilişkiyi sağlamak için
        public Cart Cart { get; set; } //navigation 
        public int CartId { get; set; } // ilişkiyi sağlamak için : hangi sepetin ürünü
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountedNewPrice { get; set; } //kullanıcı %25 indirim kodu kullandıysa

    }
}
