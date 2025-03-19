using Microsoft.EntityFrameworkCore;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class EfCoreBlogDal : EfCoreGenericRepository<Blog, DataContext>, IBlogDal
    {
        public Blog GetBlogById(int Id)
        {
            var context = new DataContext();
            {
                return context.Blogs
                             .FirstOrDefault(p => p.Id == Id);
            }
        }
        public Blog GetBlogDetails(int id)
        {
            using (var context = new DataContext())
            {
                return context.Blogs
                           .Where(p => p.Id == id)
                           .FirstOrDefault();
            }
        }

        public List<Blog> SearchBlogsByTitle(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Aradığınız içerik boş olamaz veya yalnızca boşluk karakteri içermemelidir.");
            }
            using (var context = new DataContext())
            {
                var blogs = context.Blogs.Where(blog => EF.Functions.Like(blog.SubTitle, $"%{query}%")).ToList();// case -insensitive search
                // büyük küçük harf duyarlılığı olmadan arama yapılacak

                if (blogs.Count==0)
                {
                    throw new InvalidOperationException("Aradığınız içerikte yazı bulunamamıştır.");
                }
                return blogs;
            }
        }
    }
}
