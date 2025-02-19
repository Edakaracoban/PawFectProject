using PawFect.Business.Abstract;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        private IProductDal _productDal;
        public CartManager(ICartDal cartDal, IProductDal productDal)
        {
            _cartDal = cartDal;
            _productDal = productDal;
        }
        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            var product = GetProductById(productId);

            if (product != null && product.Stock > 0)
            {
                if (quantity > product.Stock)
                {
                    quantity = product.Stock; // istenilen miktar stoktan fazla ise stok kadar eklenir.
                }

                var index = cart.CartItems.FindIndex(x => x.ProductId == productId);

                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                product.Stock -= quantity;

                _cartDal.Update(cart);
                _productDal.Update(product);
            }
            else
            {
                throw new InvalidOperationException("Ürün stokta yok ve sepete eklenemez.");
            }
        }


        public Product GetProductById(int productId)
        {
            return _productDal.GetProductById(productId);
        }

        public void ClearCart(int cartId)
        {
            _cartDal.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId); // sepet var mı kontrolü

            if (cart != null && cart.CartItems.Any(x => x.ProductId == productId))
            {
                _cartDal.DeleteFromCart(cart.Id, productId);
            }
            else
            {
                throw new Exception("Ürün sepetinizde bulunamadı.");
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetCartByUserId(userId);
        }

        public void InitialCart(string userId)
        {
            var cart = _cartDal.GetCartByUserId(userId);
            if (cart==null)
            {
                cart = new Cart() { UserId = userId };
                _cartDal.Create(cart);
            }
        }
    }
}
