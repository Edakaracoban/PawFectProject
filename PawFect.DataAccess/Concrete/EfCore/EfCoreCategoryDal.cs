﻿using Microsoft.EntityFrameworkCore;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, DataContext>, ICategoryDal
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new DataContext())
            {
                var cmd = @"delete from Category where ProductId=@p1 and CategoryId=@p0";
                context.Database.ExecuteSqlRaw(cmd, categoryId, productId);
                // Ardından Product tablosundan ürünü sil
                var cmdProduct = @"delete from Product where ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmdProduct, productId);
            }

        }

        public Category GetByIdWithProducts(int id)
        {
            using (var context = new DataContext())
            {
                return context.Categories
               .Where(i => i.Id == id)  // Gelen id ile kategori var mı?
               .Include(p => p.Products)
               .FirstOrDefault();  // Kategoriyi bulup döndürüyoruz // kategori yoksa null döner
            }
        }
        public override void Delete(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Silinecek kategori nesnesi null olamaz.");
            }
            using (var context = new DataContext())
            {
                var hasProducts = context.Products.Any(p => p.CategoryId == entity.Id);

                if (hasProducts)
                {
                    throw new InvalidOperationException("Bu kategoriye ait ürünler var, kategori silinemez.");
                }
                context.Categories.Remove(entity);
                context.SaveChanges();
            }
        }

        public Category GetByIdFind(int id)
        {
            using (var context = new DataContext())
            {
                return context.Set<Category>().FirstOrDefault(x => x.Id == id); // Id kontrolü ile
            }
        }
    }
}
