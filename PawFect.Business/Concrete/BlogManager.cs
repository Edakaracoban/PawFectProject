using PawFect.Business.Abstract;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private IBlogDal _blogDal;
        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void Create(Blog entity)
        {
            _blogDal.Create(entity);
        }

        public void Delete(Blog entity)
        {
            _blogDal.Delete(entity);
        }

        public List<Blog> GetAll()
        {
           return _blogDal.GetAll();
        }

        public Blog GetBlogById(int Id)
        {
            return _blogDal.GetBlogById(Id);
        }

        public Blog GetBlogDetails(int id)
        {
           return _blogDal.GetBlogDetails(id);
        }

        public List<Blog> SearchBlogsByTitle(string query)
        {
            return _blogDal.SearchBlogsByTitle(query);
        }

        public void Update(Blog entity)
        {
            _blogDal.Update(entity);
        }
    }
}
