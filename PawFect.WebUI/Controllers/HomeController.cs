using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Extensions;
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
            //TempData.Put("message", new ResultModel()
            //{
            //    Title = "Ho�geldiniz",
            //    Message = "Ba�lamak i�in pencereyi kapat�n",
            //    Css = "success"
            //}); // pencere direkt a��lm�yor?
            return View(new ProductListModel()
            {
                Products = products
            });
        }
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            var searchResults = _productService.SearchProductsByName(query);
            return View("Index", searchResults);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
