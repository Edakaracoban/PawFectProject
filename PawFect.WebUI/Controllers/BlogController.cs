using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Models;
using System.Drawing.Text;
using X.PagedList;
using X.PagedList.Extensions;

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
        public IActionResult BlogList(int page = 1)
        {
            int pageSize = 5; //Her sayfada 5 ürün olsun
            var blogList = _blogService.GetAll();
            var blogModelList = blogList.Select(blog => new BlogModel
            {
                Id = blog.Id,
                Header = blog.Header,
                SubTitle = blog.SubTitle,
                Image = blog.Image,
                Title = blog.Title
            }).ToList();
            IPagedList<BlogModel> pagedBlogs = blogModelList.ToPagedList(page, pageSize);  // Dönen product modelleri listeliyoruz.
            return View(pagedBlogs);
        }
        public IActionResult CreateBlog()
        {
            return View();
        }
       
    }

}
