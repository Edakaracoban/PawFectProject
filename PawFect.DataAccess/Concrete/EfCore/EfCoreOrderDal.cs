using Microsoft.EntityFrameworkCore;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, DataContext>, IOrderDal
    {
        public override void Create(Order order)
        {
            using (var context = new DataContext())
            using (var transaction = context.Database.BeginTransaction())
            //Transaction Yönetimi: Eğer ürün stok güncellemeleri ve sipariş işlemleri sırasında bir hata oluşursa, tüm işlemi geri almak için bir işlem (transaction) kullanmak iyi bir yaklaşımdır. Bu, veritabanında tutarsızlıkları önler.
            {
                try
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
                        if (product == null)
                        {
                            throw new InvalidOperationException($"Ürün {orderItem.ProductId} bulunamadı.");
                        }

                        if (product.Stock == 0)// Siparişi oluşturmak istediğimiz ürünlerin stoklarını kontrol edelim.
                        {
                            throw new InvalidOperationException($"Ürün {product.Name} stokta yok. Sipariş oluşturulamaz.");
                        }
                        product.Stock -= orderItem.Quantity; // Ürün stoktan düşülür
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            
                context.Orders.Add(order);
                context.SaveChanges();

                transaction.Commit();
            }
        }

        public Order GetOrderDetails(int id)
        {
            using (var context = new DataContext())
            {
                return context.Orders
                              .Include(o => o.OrderItems)
                              .ThenInclude(oi => oi.Product)
                              .FirstOrDefault(o => o.Id == id);
            }
        }
        public List<Order> GetOrders(string userId, string UserName)
        {
            using (var context = new DataContext())
            {
                IQueryable<Order> ordersQuery = context.Orders // Admin kullanıcı için tüm siparişler döndürülür
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product);

                if (UserName != "admin" && !string.IsNullOrEmpty(userId))
                {
                    ordersQuery = ordersQuery.Where(o => o.UserId == userId);
                }
                return ordersQuery.ToList();
            }
        }
    }
}
