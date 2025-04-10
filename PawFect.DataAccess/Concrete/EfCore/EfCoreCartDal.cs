using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PawFect.DataAccess.Concrete.EfCore//sepetle ilgili CRUD İŞLEMLERİ
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, DataContext>, ICartDal
    {
        public void ClearCart(int cartId)//sepeti içindeki tüm itemleri silme
        {
            using (var context = new DataContext())
            {
                try
                {
                    var cmd = @"delete from CartItem where CartId=@p0";
                    context.Database.ExecuteSqlRaw(cmd, cartId);
                }
                catch (Exception ex)
                {
                    throw new Exception("Sepet temizlenirken bir hata oluştu", ex);
                }
            }

        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new DataContext())
            {
                try
                {
                    var cmd = @"delete from CartItem where CartId=@p0 and ProductId=@p1";
                    context.Database.ExecuteSqlRaw(cmd, cartId, productId);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ürün silinirken bir hata oluştu", ex);
                }
            }

        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context = new DataContext())
            {
                return context.Carts
                       .Include(i => i.CartItems)
                       .ThenInclude(i => i.Product)
                       .FirstOrDefault(i => i.UserId == userId);
            }

        }

        public void Update(Cart entity)
        {
            using (var context = new DataContext())
            {
                try
                {
                    context.Carts.Update(entity);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Sepet güncellenirken bir hata oluştu", ex);
                }
            }

        }
    }
}
