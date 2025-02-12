using Microsoft.EntityFrameworkCore;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class DataContext : DbContext
    {
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // connection string e bağlanmak için Oncongifuring metodu kullanılır.
            optionsBuilder.UseSqlServer(@"Server=EDANIN-DESKTOPU\SQLEXPRESS;Database=PAWFECT;uid=sa;pwd=1;TrustServerCertificate=True");
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)  // Her Product bir Category'ye sahiptir
            .WithMany(c => c.Products)  // Her Category birden fazla Product'a sahip olabilir
            .HasForeignKey(p => p.CategoryId)  // CategoryId, Product tablosunda yabancı anahtar olur
            .OnDelete(DeleteBehavior.Restrict);  // Silme davranışını belirleyebilirsiniz

            modelBuilder.Entity<OrderItem>()
             .Property(o => o.Price)
             .HasColumnType("decimal(18,2)");  // OrderItem Price için uygun tür

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");  // Product Price için uygun tür
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
