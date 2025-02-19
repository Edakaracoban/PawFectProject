using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Abstract
{
    public interface ICartService // public olmak zorundadır.Çünkü başka katmanlar da bu interface'i kullanacak.
    {
        void InitialCart(string userId); // Kartı ilk haline getir.
        Cart GetCartByUserId(string userId); // Kullanıcıya ait sepeti getir.
        void AddToCart(string userId, int productId, int quantity); // Ürün ekleme
        void DeleteFromCart(string userId, int productId); // Sepetten ürün silme
        void ClearCart(int cartId); // Sepeti temizleme
        Product GetProductById(int productId); // Ürünü id'sine göre getirme
    }
}
