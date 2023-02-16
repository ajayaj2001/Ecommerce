using Order.Entities.Dtos;
using System;
using System.Collections.Generic;

namespace Order.Contracts.Services
{
    public interface IApiService
    {
        ///<summary>
        ///get product by ids
        ///</summary>
        ///<param name="productIds"></param>
        ///<param name="token"></param>
        List<ResultProductDto> GetProductByIds(List<Guid> productIds, string token);

        ///<summary>
        ///get single product by id
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="token"></param>
        ResultProductDto GetProductById(Guid productId, string token);

        ///<summary>
        ///update product by ids
        ///</summary>
        ///<param name="productDetails"></param>
        ///<param name="token"></param>
        bool UpdateProductByIds(List<UpdateProductQuantityDto> productDetails, string token);
    }
}
