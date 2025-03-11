using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.WebUI.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace PawFect.WebUI.Controllers
{

    public class AdminController : Controller
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult ProductList(int page = 1)
        {
            int pageSize = 10; //Her sayfada 10 ürün olsun

            var productList = _productService.GetAll(); 
          
            var productModelList = productList.Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Category = product.Category 
            }).ToList();

            IPagedList<ProductModel> pagedProducts = productModelList.ToPagedList(page, pageSize);  // Dönen product modelleri listeliyoruz.

            return View(pagedProducts);
        }
    }


}