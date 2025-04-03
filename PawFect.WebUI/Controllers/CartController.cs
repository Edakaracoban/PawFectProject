using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Identity;
using PawFect.WebUI.Models;

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

    }
}
