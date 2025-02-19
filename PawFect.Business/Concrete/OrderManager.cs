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
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public void Create(Order entity)
        {
           _orderDal.Create(entity);
        }

        public Order GetOrderDetails(int id)
        {
            return _orderDal.GetOrderDetails(id);
        }

        public List<Order> GetOrders(string userId, string UserName)
        {
            return _orderDal.GetOrders(userId, UserName);
        }
    }
}
