using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Abstract
{
    public interface IProductDal: IRepository<Product>
    {
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category); //Kategoriye göre ürünleri getirir
        void Update(Product entity, int categoryId);
        List<Product> SearchProductsByName(string query); // string query parametresi ile ürün arama yapılacak
        Product GetProductById(int productId);
        List<Product> GetProductsByCategoryId(int? categoryId);
    }
}
