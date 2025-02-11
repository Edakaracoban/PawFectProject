using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }
        public string PaymentId { get; set; }
        public string PaymentToken { get; set; } //haberleşme için kriptolaşmış token
        public string ConversionId { get; set; } //kur bazlı çalışma    convertionid 1 = TR
        public EnumPaymentTypes PaymentTypes { get; set; }
        public List<OrderItem> OrderItems { get; set; } //siparişin içindeki kalemler
        public Order()
        {
            OrderItems = new List<OrderItem>(); //sipariş oluşturulduğunda liste boş olarak oluşturulacak
        }
    }
    public enum EnumPaymentTypes
    {
        CreditCard = 0,
        Eft = 1
    }

}

