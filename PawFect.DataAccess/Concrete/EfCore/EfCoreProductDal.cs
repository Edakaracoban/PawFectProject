using Microsoft.EntityFrameworkCore;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, DataContext>, IProductDal
    {
        public Product GetProductDetails(int id)
        {
           using (var context = new DataContext())
            {
                return context.Products
                           .Where(p => p.Id == id)
                           .Include(p => p.Category) 
                           .Include("Comments")  
                           .FirstOrDefault(); 
            }
        }

        public List<Product> GetProductsByCategory(string category)
        {
            using (var context = new DataContext())
            {
                if (string.IsNullOrWhiteSpace(category))
                {
 
                    return context.Products.Include(p => p.Category).ToList();
                }
                return context.Products
                              .Include(p => p.Category)
                              .Where(p => p.Category.Name.ToLower().Contains(category.ToLower())) 
                              .ToList();
            }
        }


        public List<Product> GetProductsByCategoryId(int? categoryId)
        {
            using (var context = new DataContext())
            {
                IQueryable<Product> productsQuery = context.Products.Include(p => p.Category);
                if (categoryId.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
                }
                return productsQuery.ToList();
            }
        }

        public List<Product> SearchProductsByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Product>(); 
            }

            using (var context = new DataContext())
            {
                return context.Products    
                             .Where(p => EF.Functions.Like(p.Name, $"%{query}%")) // case-insensitive search büyük küçük harf duyarlılığı olmadan arama yapılacak
                              .ToList();
            }
        }

        public void Update(Product entity, int categoryId)
        {
            using (var context = new DataContext())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == entity.Id);
                if (product != null)
                {
                    if (!string.IsNullOrEmpty(product.Image) && product.Image != entity.Image)
                    {
                        // Eski resim varsa ve yeni resimle farklı ise, eski resmi sil
                        DeleteOldImage(product.Image); // Resim silme fonksiyonu
                    }
                    product.Name = entity.Name;
                    product.Stock = entity.Stock;
                    product.Price = entity.Price;
                    product.Image = entity.Image;
                    product.Description = entity.Description;
                    product.CategoryId = categoryId;

                    product.Category = context.Categories.FirstOrDefault(c => c.Id == categoryId);

                    context.SaveChanges();
                }
            }
        }
        private void DeleteOldImage(string oldImagePath)
        {
            try
            {
                // Burada dosyanın kaydedildiği yolu belirtmeniz gerekiyor
                string fullPath = Path.Combine("wwwroot\\img", oldImagePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Resim silinirken hata oluştu: {ex.Message}");
            }
        }

        public override void Delete(Product entity)
        {
            using (var context = new DataContext())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == entity.Id);

                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Silinecek ürün bulunamadı.");
                }
            }
        }

        public Product GetProductById(int productId)
        {
            var context = new DataContext();
            {
                return context.Products
                             .Include(p => p.Category)
                             .FirstOrDefault(p => p.Id == productId);
            }
        }
    }
}
