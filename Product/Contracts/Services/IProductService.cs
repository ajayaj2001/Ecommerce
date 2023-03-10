using Order.Entities.Dtos;
using Product.Entities.Dtos;
using Product.Entities.Models;
using System;
using System.Collections.Generic;

namespace Product.Contracts.Services
{
    public interface IProductService
    {
        ///<summary>
        ///create new product in db
        ///</summary>
        ///<param name="productDetails"></param>
        ///<param name="authId"></param>
        Guid CreateProduct(CreateProductDto productDetails, Guid authId);

        ///<summary>
        ///get category id by category name
        ///</summary>
        ///<param name="name"></param>
        Category GetCategoryByName(string name);

        ///<summary>
        ///get all product based on filter
        ///</summary>
        ///<param name="pageSortParam"></param>
        List<ResultProductDto> GetAllProducts(PageSortParam pageSortParam, string role);

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="productId"></param>
        ProductDto GetDetailedProductById(Guid productId);

        ///<summary>
        ///get product by product id
        ///</summary>
        ///<param name="productId"></param>
        ProductDetail GetProductById(Guid productId);

        ///<summary>
        ///get product list by product ids
        ///</summary>
        ///<param name="productIds"></param>
        List<ResultProductDto> GetProductByIds(List<Guid> productIds);

        ///<summary>
        ///delete product in database
        ///</summary>
        ///<param name="productId"></param>
        void DeleteProduct(Guid productId);

        ///<summary>
        ///update product 
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="productId"></param>
        ///<param name="productInput"></param>
        ///param name="categoryId"></param>
        void UpdateProduct(UpdateProductDto productInput, Guid productId, Guid authId, Guid categoryId);

        ///<summary>
        ///update product list 
        ///</summary>
        ///<param name="authId"></param>
        void UpdateProductList(List<UpdateProductQuantityDto> products);
    }
}
