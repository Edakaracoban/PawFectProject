using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.WebUI.Models;

namespace PawFect.WebUI.ViewComponents
{
    public class _BlogViewComponents : ViewComponent
    {
        private IBlogService _blogService;

        public _BlogViewComponents(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs =  _blogService.GetAll();
            var blogModels = blogs.Select(blog => new BlogModel
            {
                Id = blog.Id,
                Image = blog.Image,
                Header = blog.Header,
                Title = blog.Title,
                SubTitle = blog.SubTitle
            }).ToList();

            return View(blogModels);
        }
    }
}
