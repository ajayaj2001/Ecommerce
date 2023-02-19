using Microsoft.EntityFrameworkCore;
using System;
using Product.Entities.Models;
using System.IO;

namespace Product.DbContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public void OnBeforeSaving(Guid UserId)
        {
            System.Collections.Generic.IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> productEntries = ChangeTracker.Entries();
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry in productEntries)
            {
                if (entry.Entity is BaseModel customer)
                {
                    Guid user = UserId;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            customer.UpdatedAt = new DateTime().ToString();
                            customer.UpdatedBy = user;
                            break;
                        case EntityState.Added:
                            customer.CreatedAt = new DateTime().ToString();
                            customer.CreatedBy = user;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public DbSet<ProductDetail> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string productPath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Product.csv");
            string[] productValues = File.ReadAllText(Path.GetFullPath(productPath)).Split('\n');

            foreach (string item in productValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {

                        ProductDetail product = new ProductDetail()
                        {
                            Id = Guid.Parse(row[0]),
                            Name = row[1],
                            Description = row[2],
                            Quantity = int.Parse(row[3]),
                            Visibility = bool.Parse(row[4]),
                            CategoryId = Guid.Parse(row[5]),
                        };
                        modelBuilder.Entity<ProductDetail>().HasData(product);
                    }
                }
            }
            string categoryPath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Category.csv");
            string[] categoryValues = File.ReadAllText(Path.GetFullPath(categoryPath)).Split('\n');

            foreach (string item in categoryValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        Category category = new Category()
                        {
                            Id = Guid.Parse(row[0]),
                            Name = row[1],
                            Description = row[2],
                        };

                        modelBuilder.Entity<Category>().HasData(category);
                    }
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
