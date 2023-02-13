using Product.Entities.Models;
using System;

namespace Product.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        ///<summary>
        ///get category id by categroy name
        ///</summary>
        ///<param name="name"></param>
        Category GetTypeByName(string name);

        ///<summary>
        ///get category name by category id
        ///</summary>
        ///<param name="id"></param>
        Category GetTypeById(Guid id);
    }
}
