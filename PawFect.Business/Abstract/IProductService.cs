using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetProductsByCategory(string category); //Kategoriye göre ürünleri getirir
        List<Product> GetAll();
        Product GetProductDetails(int id);
        void Create(Product entity);
        void Update(Product entity, int categoryId);
        void Delete(Product entity);
        List<Product> SearchProductsByName(string query, string category); // string query parametresi ile ürün arama yapılacak
        public List<Product> GetProductsByCategoryId(int? categoryId);
    }
}
