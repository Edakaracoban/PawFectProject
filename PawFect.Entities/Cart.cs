using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Entities
{
    public class Cart
    {
        //sepet
        public int CartId { get; set; } //sepetin idsi
        public string UserId { get; set; } //Kullanıcı id
        public List<CartItem> CartItems { get; set; } //sepetten sepetin içindeki elemanlara erişmek için
        
    }
}
