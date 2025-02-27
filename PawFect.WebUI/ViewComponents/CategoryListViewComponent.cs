using PawFect.Business.Abstract;
using PawFect.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PawFect.WebUI.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
                _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(
                new CategoryListViewModel()
                {
                    
                    Categories = _categoryService.GetAll()
                }
                
                );
        }
    }
}
