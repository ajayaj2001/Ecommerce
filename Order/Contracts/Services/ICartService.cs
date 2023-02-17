using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;

namespace Order.Contracts.Services
{
    public interface ICartService
    {
        ///<summary>
        ///create new cart in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="cartDetail"></param>
        Guid AddToCart(CreateCartDto cartDetail, Guid authId);

        ///<summary>
        ///fetch cart in database
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="token"></param>
        List<ReturnCartDto> GetCartForUser(Guid userId, string token);

        ///<summary>
        ///check if product exist in cart
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
        bool checkProductExist(Guid productId, Guid userId);

        ///<summary>
        ///delete product from cart
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="authId"></param>
        void DeleteCartProduct(Guid productId, Guid authId);

        ///<summary>
        ///update product on cart
        ///</summary>
        ///<param name="cartInput"></param>
        ///<param name="authId"></param>
        void UpdateCartProduct(CreateCartDto cartInput, Guid authId);

        ///<summary>
        ///fetch cart details in database
        ///</summary>
        ///<param name="userId"></param>
        IEnumerable<Cart> GetCartDetails(Guid userId);

        ///<summary>
        ///move cart to order
        ///</summary>
        ///<param name="cartDetails"></param>
        ///<param name="authId"></param>
        ///<param name="token"></param>
        string UpdateOrderIdToCart(List<Cart> cartDetails, Guid authId, string token);

        ///<summary>
        ///update product by ids
        ///</summary>
        ///<param name="productDetails"></param>
        ///<param name="token"></param>
        bool UpdateProductByIds(List<UpdateProductQuantityDto> productDetails, string token);

        ///<summary>
        ///get single product by id
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="token"></param>
        ResultProductDto GetProductById(Guid productId, string token);

        ///<summary>
        ///get product by ids
        ///</summary>
        ///<param name="productIds"></param>
        ///<param name="token"></param>
        List<ResultProductDto> GetProductByIds(List<Guid> productIds, string token);
    }
}
