using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.Entities;
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
        public IActionResult Details(int? id)//int? ifadesi id parametresinin null olabilmesini sağlar.
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product product = _productService.GetProductDetails(id.Value); //id null olamadığı için value özelliği kullanılır.
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailsModel()
            {
                Product = product,
                Comments = product.Comments
            });
        }

    }
}
