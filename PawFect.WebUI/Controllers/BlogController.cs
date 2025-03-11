using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Models;
using System.Drawing.Text;

namespace PawFect.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
             Blog blog = _blogService.GetBlogDetails(id.Value);
            if (blog == null)
            {
                return NotFound();
            }
            return View(new BlogModel()
            {
                Id = id.Value,
                Header=blog.Header,
                SubTitle=blog.SubTitle,
                Image=blog.Image,
                Title=blog.Title,
            });
        }
       
    }
}
