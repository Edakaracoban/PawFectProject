using PawFect.Entities;

namespace PawFect.WebUI.Models
{
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public decimal TotalPrice()
        {
            decimal totalPrice = OrderItems?.Sum(x => x.Price * x.Quantity) ?? 0;
            return totalPrice;
        }
 
    }
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
    
