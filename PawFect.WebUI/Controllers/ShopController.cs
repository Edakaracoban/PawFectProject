using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.WebUI.Models;

namespace PawFect.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("products/{category?}")]
        public IActionResult List(string category, int page = 1)
        {
            

            return View();
        }

        
    }
}
