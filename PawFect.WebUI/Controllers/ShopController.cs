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
            const int pageSize = 5;
            var products = new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    ItemsPerPage = pageSize,
                    CurrentCategory = category,
                    CurrentPage = page
                },
                Products = _productService.GetProductByCategory(category, page, pageSize)
            };
            return View(products);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Product product = _productService.GetProductDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailsModel()
            {
                Product = product,
                Category = product.Category,
                CategoryId = product.CategoryId,
                Comments = product.Comments ?? new List<Comment>() // null kontrolü ekliyoruz
            });
        }
    }
}
