using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Models;
using System.Diagnostics;

namespace PawFect.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
             
            //var categories = _categoryService.GetAll();
            //ViewBag.Category = new SelectList(categories, "Id", "Name");
            var products = _productService.GetAll();
            if (products == null || !products.Any())
            {
                products = new List<Product>();
            }
            return View(new ProductListModel()
            {

                Products = products
            });
        }
        [HttpPost]
        public async Task<IActionResult> Search(string query, string category)
        {
            if (string.IsNullOrWhiteSpace(query) && string.IsNullOrWhiteSpace(category))
            {
                return RedirectToAction("Index");
            }

            // Search by query and/or category
            var searchResults = _productService.SearchProductsByName(query, category); // Assuming the service method takes both parameters

            var model = new ProductListModel
            {
                Products = searchResults.ToList()
            };

            return View("Search", model);
        }

        public IActionResult Search()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
