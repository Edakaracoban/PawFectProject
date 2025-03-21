using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Abstract
{
    public interface IProductDal: IRepository<Product>
    {
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category); //Kategoriye göre ürünleri getirir
        void Update(Product entity, int categoryId);
        List<Product> SearchProductsByName(string query,string category); // string query parametresi ile ürün arama yapılacak
        Product GetProductById(int productId);
        List<Product> GetProductsByCategoryId(int? categoryId);
        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null);
        int GetCountByCategory(string category);
        List<Product> GetProductsByCategory(string category, int page, int pageSize); //pagination çok data gelince kull.

    }
}
