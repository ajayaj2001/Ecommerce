using System;
using System.Collections.Generic;
using System.Linq;
using Product.Contracts.Repositories;
using Product.DbContexts;
using Product.Entities.Models;

namespace Product.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///to create product in db
        ///</summary>
        ///<param name="product"></param>
        public void CreateProduct(ProductDetail product)
        {
            _context.Products.Add(product);
        }

        ///<summary>
        ///save all changes
        ///</summary>
        public bool Save(Guid authId)
        {
            _context.OnBeforeSaving(authId);
            return _context.SaveChanges() >= 0;
        }

        ///<summary>
        ///get all product from db
        ///</summary>
        public IEnumerable<ProductDetail> GetAllProducts()
        {
            return _context.Products.Where(a => a.IsActive).ToList();
        }

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="id"></param>
        public ProductDetail GetProductById(Guid id)
        {
            return _context.Products.FirstOrDefault(b => b.Id == id && b.IsActive);
        }

        ///<summary>
        ///update product in db
        ///</summary>
        ///<param name="product"></param>
        public void UpdateProduct(ProductDetail product)
        {
            _context.Products.Update(product);
        }
    }
}
