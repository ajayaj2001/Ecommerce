﻿using Product.Entities.Dtos;
using Product.Entities.Models;
using System;
using System.Collections.Generic;

namespace Product.Contracts.Repositories
{
    public interface IProductRepository
    {

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="user"></param>
        // user operation
        void CreateProduct(ProductDetail product);


        ///<summary>
        ///save all changes
        ///</summary>
        bool Save();

        ///<summary>
        ///get all product from db
        ///</summary>
        IEnumerable<ProductDetail> GetAllProducts();

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="id"></param>
        ProductDetail GetProductById(Guid id);

        ///<summary>
        ///update product in db
        ///</summary>
        ///<param name="product"></param>
        void UpdateProduct(ProductDetail product);
    }

}
