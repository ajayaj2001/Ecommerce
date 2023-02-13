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
        ///get product by product id
        ///</summary>
        ///<param name="productIds"></param>
        List<ProductDto> GetProductByIds(List<Guid> productIds);

        ///<summary>
        ///delete product in database
        ///</summary>
        ///<param name="productId"></param>
        void DeleteProduct(Guid productId);

        ///<summary>
        ///update address book details
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="productFromRepo"></param>
        ///<param name="productInput"></param>
        ///param name="categoryId"></param>
        void UpdateProduct(UpdateProductDto productInput, ProductDetail productFromRepo, Guid authId, Guid categoryId);
    }
}
