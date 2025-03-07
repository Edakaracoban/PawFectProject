using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Abstract
{
    public interface IBlogService
    {
        List<Blog> SearchBlogsByTitle(string query); // string query parametresi ile ürün arama yapılacak
        void Create(Blog entity);
        void Update(Blog entity);
        void Delete(Blog entity);
        List<Blog> GetAll();
        Blog GetBlogDetails(int id);
    }
}
