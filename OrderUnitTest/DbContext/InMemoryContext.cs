using Microsoft.EntityFrameworkCore;
using System;
using Order.DbContexts;
using System.IO;
using Order.Entities.Models;

namespace ProductUnitTest.InMemoryContext
{
    public static class InMemorydbContext
    {
        /// <summary>
        /// This method is used to create the InMemeorydatabase
        /// </summary>
        public static OrderContext orderContext()
        {
            DbContextOptions<OrderContext> options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            OrderContext context = new OrderContext(options);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string ecommercePath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Wishlist.csv");
            string[] wishlistValues = File.ReadAllText(Path.GetFullPath(ecommercePath)).Split('\n');

            foreach (string item in wishlistValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        context.WishLists.Add(new WishList()
                        {
                            Id = Guid.Parse(row[0]),
                            Name = row[1],
                            ProductId = Guid.Parse(row[2]),
                            UserId = Guid.Parse(row[3]),
                        });
                    }
                }
            }

            string cartPath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Cart.csv");
            string[] cartValues = File.ReadAllText(Path.GetFullPath(cartPath)).Split('\n');

            foreach (string item in cartValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        context.Carts.Add(new Cart() { Id = Guid.Parse(row[0]), Quantity = int.Parse(row[1]), ProductId = Guid.Parse(row[2]), UserId = Guid.Parse(row[3]) });
                    }
                }
            }

            context.SaveChanges();

            return context;
        }
    }
}