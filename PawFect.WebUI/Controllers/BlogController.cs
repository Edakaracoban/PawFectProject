﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                Header = blog.Header,
                SubTitle = blog.SubTitle,
                Image = blog.Image,
                Title = blog.Title,
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
            return View(new BlogModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var entity = new Blog()
                {
                    Id = model.Id,
                    Header = model.Header,
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Image = model.Image
                };
                _blogService.Create(entity);
                return RedirectToAction("BlogList", "Blog");
            }
            return View(model);
        }

        public IActionResult EditBlog(int id)

        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _blogService.GetBlogDetails(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new BlogModel()
            {
                Id = entity.Id,
                Header = entity.Header,
                Title = entity.Title,
                SubTitle = entity.SubTitle,
                Image = entity.Image
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(BlogModel model)
        {
            var entity = _blogService.GetBlogById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.SubTitle = model.SubTitle;
            entity.Title = model.Title;
            entity.Header = model.Header;

            entity.Image = model.Image;
            _blogService.Update(entity);
            return RedirectToAction("BlogList", "Blog");
        }

        [HttpPost]
        public IActionResult DeleteBlog(int blogId)
        {
            ;
            var blog = _blogService.GetBlogById(blogId);
            if (blog != null)
            {
                _blogService.Delete(blog);
            }
            return RedirectToAction("BlogList","Blog");
        }
    }

}
