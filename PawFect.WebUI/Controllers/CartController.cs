using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Identity;
using PawFect.WebUI.Models;
using PawFect.WebUI.Session;
using System.Drawing.Text;
using System.Net;

namespace PawFect.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IProductService _productService;
        private IOrderService _orderService;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, IProductService productService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x => new CartItemModel()
                {
                    CartItemId = x.Id,
                    Name = x.Product.Name,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Product.Price,
                    Image = x.Product.Image
                }).ToList()
            });
        }
        public IActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User), productId, quantity);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            OrderModel orderModel = new OrderModel();
            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x => new CartItemModel()
                {
                    CartItemId = x.Id,
                    ProductId = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Image = x.Product.Image,
                    Quantity = x.Quantity
                }).ToList()
            };
            return View(orderModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel orderModel, string paymentMethod)
        {
            ModelState.Remove("CartModel");

            if (ModelState.IsValid) //önyüzde alanlar dolduruldu mu?
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);

                orderModel.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(x => new CartItemModel()
                    {
                        CartItemId = x.Id,
                        ProductId = x.Product.Id,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        Image = x.Product.Image,
                        Quantity = x.Quantity
                    }).ToList()
                };

                if (paymentMethod == "credit")
                {
                    var payment = PaymentProccess(orderModel);
                    if (payment.Result.Status == "success")
                    {
                        SaveOrder(orderModel, userId);
                        ClearCart(cart.Id.ToString());
                        TempData["Message"] = "Your order has been completed successfully";
                    }
                    else
                    {
                        TempData["Message"] = "Your order could not be completed";
                    }
                }
                else //eft için olan kısımda payment proccess kullanılmıyor.
                {
                    SaveOrder(orderModel, userId);
                    ClearCart(cart.Id.ToString());
                    TempData["Message"] = "Your order has been completed successfully";
                }
            }
            return RedirectToAction("Index", "Cart");
        }
        private void ClearCart(string cartId)
        {
            _cartService.ClearCart(Convert.ToInt32(cartId));
        }
        //iyzikoyu kullanarak istek atar.//iyzipay kütüphanesi kullanılır.
        private async Task<Payment> PaymentProccess(OrderModel orderModel)
        {
            Options options = new Options()
            {
                BaseUrl = "https://sandbox-api.iyzipay.com", //test ortamı
                ApiKey = "sandbox-cNnJEaoyNt0sCREL4nOq8PajTLQwWeXz",
                SecretKey = "sandbox-cmJxJfaGlVarqNV3c5ZQcMTwVNh8qswx"
            };

            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString); //dış Ip

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString(); //dil
            request.ConversationId = Guid.NewGuid().ToString(); //her işlem için benzersiz bir id oluşturur.
            request.Price = orderModel.CartModel.TotalPrice().ToString().Split(',')[0];//toplam fiyat
            request.PaidPrice = orderModel.CartModel.TotalPrice().ToString().Split(',')[0]; //ödenecek fiyat
            request.Currency = Currency.TRY.ToString(); //para birimi
            request.Installment = 1; //taksit sayısı
            request.BasketId = orderModel.CartModel.CartId.ToString(); //sepet id
            request.PaymentChannel = PaymentChannel.WEB.ToString(); //ödemeyi nereden yapıyor
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString(); //ürün grubu

            PaymentCard paymentCard = new PaymentCard()
            {
                CardHolderName = orderModel.CardName,
                CardNumber = orderModel.CardNumber,
                ExpireMonth = orderModel.ExprationMonth,
                ExpireYear = orderModel.ExprationYear,
                Cvc = orderModel.CVV,
                RegisterCard = 0
            };
            request.PaymentCard = paymentCard; //kart bilgileri

            Buyer buyer = new Buyer()
            {
                Id =_userManager.GetUserId(User),
                Name = orderModel.FirstName,
                Surname = orderModel.LastName,
                GsmNumber = orderModel.Phone,
                Email = orderModel.Email,
                IdentityNumber = "12345678901",
                RegistrationAddress = orderModel.Address,
                Ip = externalIp.ToString(),
                City = orderModel.City,
                Country = "Turkey",
                ZipCode = "34000"
            };
            request.Buyer = buyer; //alıcı bilgileri

            Address address = new Address()
            {
                ZipCode = "34000",
                ContactName = orderModel.FirstName + " " + orderModel.LastName,
                City = orderModel.City,
                Country = "Turkey",
                Description = orderModel.Address
            };
            request.ShippingAddress = address; //gönderim adresi
            request.BillingAddress = address; //fatura adresi

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            foreach (var cartItem in orderModel.CartModel.CartItems)
            {
                basketItem = new BasketItem()
                {
                    Id = cartItem.ProductId.ToString(),
                    Name = cartItem.Name,
                    Category1 = _productService.GetProductDetails(cartItem.ProductId).Category.Id.ToString(),
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = (cartItem.Price * cartItem.Quantity).ToString().Split(',')[0]
                };
                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems; //sepet ürünleri

            Payment payment = await Payment.Create(request, options); //ödemeyi oluşturur.
            if (payment.Status == "success")
            {
                return payment;
            }
            else
            {
                return null;
            }
        }
        // EFT
        private void SaveOrder(OrderModel model, string userId)
        {
            Order order = new Order()
            {
                OrderNumber = Guid.NewGuid().ToString(),
                PaymentTypes = EnumPaymentTypes.Eft,
                PaymentToken = Guid.NewGuid().ToString(),
                ConversionId = Guid.NewGuid().ToString(),
                PaymentId = Guid.NewGuid().ToString(),
                OrderNote = model.OrderNote,
                OrderDate = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Address = model.Address,
                City = model.City,
                Email = model.Email,
                Phone = model.Phone,
                UserId = userId
            };

            foreach (var cartItem in model.CartModel.CartItems)
            {
                var orderItem = new Entities.OrderItem()
                {
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    ProductId = cartItem.ProductId,
                };

                order.OrderItems.Add(orderItem);
            }

            _orderService.Create(order);

        }

        // Credit Card
        private void SaveOrder(OrderModel model, Task<Payment> payment, string userId)
        {
            Order order = new Order()
            {
                OrderNumber = Guid.NewGuid().ToString(),
                PaymentTypes = EnumPaymentTypes.CreditCard,
                PaymentToken = Guid.NewGuid().ToString(),
                ConversionId = payment.Result.ConversationId,
                PaymentId = payment.Result.PaymentId,
                UserName = model.UserName,
                OrderNote = model.OrderNote,
                OrderDate = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                Email = model.Email,
                Phone = model.Phone,
                UserId = userId
            };

            foreach (var cartItem in model.CartModel.CartItems)
            {
                var orderItem = new Entities.OrderItem()
                {
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    ProductId = cartItem.ProductId,
                };

                order.OrderItems.Add(orderItem); //sepetin içindeki item değerlerini orderitem'a ekle
            }

            _orderService.Create(order);
        }
        public IActionResult GetOrders()
        {
            var userId = _userManager.GetUserId(User);
            var UserName = _userManager.GetUserName(User);

            var orders = _orderService.GetOrders(userId, UserName);


            var orderListModel = new List<OrderListModel>();

            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = order.Id;
                orderModel.Address = order.Address;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.PaymentTypes = order.PaymentTypes;
                orderModel.OrderNote = order.OrderNote;
                orderModel.City = order.City;
                orderModel.Email = order.Email;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Phone = order.Phone;
                orderModel.UserName = order.UserName;

                orderModel.OrderItems = order.OrderItems.Select(x => new OrderItemModel()
                {
                    OrderItemId = x.Id,
                    Name = x.Product.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Image = x.Product.Image

                }).ToList();

                orderListModel.Add(orderModel);
            }
            return View(orderListModel);
        }

    }
}
