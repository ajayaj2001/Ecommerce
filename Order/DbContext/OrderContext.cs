using Microsoft.EntityFrameworkCore;
using System;
using Order.Entities.Models;
using System.IO;

namespace Order.DbContexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
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


        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string wishlistPath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Wishlist.csv");
            string[] wishlistValues = File.ReadAllText(Path.GetFullPath(wishlistPath)).Split('\n');

            foreach (string item in wishlistValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        WishList wishList = new WishList()
                        {
                            Id = Guid.Parse(row[0]),
                            Name = row[1],
                            ProductId =Guid.Parse(row[2]),
                            UserId = Guid.Parse(row[3]),
                        };
                        modelBuilder.Entity<WishList>().HasData(wishList);
                    }
                }
            }
            string cartPath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Cart.csv");
            string[] carttValues = File.ReadAllText(Path.GetFullPath(cartPath)).Split('\n');

            foreach (string item in carttValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        Cart cart = new Cart() { Id = Guid.Parse(row[0]), Quantity = int.Parse(row[1]), ProductId = Guid.Parse(row[2]), UserId = Guid.Parse(row[3]) };
                        modelBuilder.Entity<Cart>().HasData(cart);
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
