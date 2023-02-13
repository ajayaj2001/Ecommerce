using System;
using System.Linq;
using Product.Contracts.Repositories;
using Product.DbContexts;
using Product.Entities.Models;

namespace Product.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductContext _context;

        public CategoryRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///get category id by categroy name
        ///</summary>
        ///<param name="name"></param>
        public Category GetTypeByName(string name)
        {

            return _context.Categories.FirstOrDefault(b => b.Name == name && b.IsActive);
        }

        ///<summary>
        ///get category name by category id
        ///</summary>
        ///<param name="id"></param>
        public Category GetTypeById(Guid id)
        {
            return _context.Categories.FirstOrDefault(b => b.Id == id && b.IsActive);
        }

    }
}
