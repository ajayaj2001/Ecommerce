using Microsoft.EntityFrameworkCore;
using System;
using Order.Entities.Models;

namespace Order.DbContexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid categoryId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4622");
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            Guid userId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");

            WishList wishList = new WishList()
            {
                Id = categoryId,
                Name = "Personal",
                ProductId = productId,
                UserId = userId,
            };

            Cart cart = new Cart() { Id = categoryId, Quantity = 2, ProductId = productId, UserId = userId };

            modelBuilder.Entity<WishList>().HasData(wishList);
            modelBuilder.Entity<Cart>().HasData(cart);

            base.OnModelCreating(modelBuilder);
        }
    }
}
