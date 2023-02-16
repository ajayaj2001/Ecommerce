using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Product.DbContexts;
using System.IO;
using Product.Entities.Models;
using System.Reflection.Emit;

namespace ProductUnitTest.InMemoryContext
{
    public static class InMemorydbContext
    {
        /// <summary>
        /// This method is used to create the InMemeorydatabase
        /// </summary>
        public static ProductContext productContext()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new ProductContext(options);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string ecommercePath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Product.csv");
            string[] productValues = File.ReadAllText(Path.GetFullPath(ecommercePath)).Split('\n');

            foreach (string item in productValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        context.Products.Add(new ProductDetail()
                        {
                            Id = Guid.Parse(row[0]),
                            Name = row[1],
                            Description = row[2],
                            Quantity = int.Parse(row[3]),
                            Visibility = bool.Parse(row[4]),
                            CategoryId = Guid.Parse(row[5]),
                        });
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
                        context.Categories.Add(new Category()
                        {
                            Id = Guid.Parse(row[0]),
                            Name = row[1],
                            Description = row[2],
                        });
                    }
                }
            }

            context.SaveChanges();

            return context;
        }
    }
}