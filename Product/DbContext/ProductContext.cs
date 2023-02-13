using Microsoft.EntityFrameworkCore;
using System;
using Product.Entities.Models;

namespace Product.DbContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<ProductDetail> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid categoryId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4622");
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            Guid userId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");


            ProductDetail product = new ProductDetail()
            {
                Id = productId,
                Name = "Orange",
                Description = " fresh orange fruit",
                Quantity = 20,
                Visibility = true,
                CategoryId = categoryId,
            };

            Category category = new Category()
            {
                Id = categoryId,
                Name = "Fruit",
                Description = "fruit category",
            };

            modelBuilder.Entity<Category>().HasData(category);
            modelBuilder.Entity<ProductDetail>().HasData(product);

            base.OnModelCreating(modelBuilder);
        }
    }
}
