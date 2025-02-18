using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Abstract
{
    public interface IBlogDal : IRepository<Blog>
    {
        List<Blog> SearchBlogsByTitle(string query); // string query parametresi ile ürün arama yapılacak
    }
}
