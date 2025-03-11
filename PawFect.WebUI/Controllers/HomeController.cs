using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Models;
using System.Diagnostics;

namespace PawFect.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {  
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
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            var searchResults = _productService.SearchProductsByName(query);


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
