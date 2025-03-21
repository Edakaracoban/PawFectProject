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
    public class ProductManager :IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Create(Product entity)
        {
            _productDal.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public List<Product> GetProductByCategory(string category, int page, int pageSize)
        {
            return _productDal.GetProductsByCategory(category, page, pageSize);
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(string category)
        {
            return _productDal.GetProductsByCategory(category);
        }

        public List<Product> GetProductsByCategoryId(int? categoryId)
        {
            return _productDal.GetProductsByCategoryId(categoryId);
        }

        public List<Product> SearchProductsByName(string query, string category)
        {
            return _productDal.SearchProductsByName(query,category);
        }

        public void Update(Product entity, int categoryId)
        {
            _productDal.Update(entity, categoryId);
        }
    }
}
