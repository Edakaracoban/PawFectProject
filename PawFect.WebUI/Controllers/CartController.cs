using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Identity;
using PawFect.WebUI.Models;
using System.Drawing.Text;

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

                if (paymentMethod=="credit")
                {
                    var payment = PaymentProccess(orderModel);
                    if (payment.Result.Status =="success")
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
            return View(orderModel);
        }
        private void ClearCart(string cartId)
        {
            _cartService.ClearCart(Convert.ToInt32(cartId));
        } 
    }
}
