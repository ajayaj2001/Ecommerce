using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;

namespace Order.Contracts.Services
{
    public interface ICartService
    {
        ///<summary>
        ///create new wishlist in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="cartDetail"></param>
        Guid AddToCart(CreateCartDto cartDetail, Guid authId);

        ///<summary>
        ///fetch cart in database
        ///</summary>
        ///param name="userId"></param>
        List<ReturnCartDto> GetCartForUser(Guid userId);

        ///<summary>
        ///check if wishlist name already added to datbase
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
        bool checkProductExist(Guid productId, Guid userId);

        ///<summary>
        ///delete wishlist in database
        ///</summary>
        ///<param name="productId"></param>
        ///param name="authId"></param>
        void DeleteCartProduct(Guid productId, Guid authId);

        ///<summary>
        ///update product on cart
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="cartInput"></param>
         void UpdateCartProduct(CreateCartDto cartInput, Guid authId);

        ///<summary>
        ///fetch cart in database
        ///</summary>
        ///param name="userId"></param>
        IEnumerable<Cart> GetCartDetails(Guid userId);

        ///<summary>
        ///move cart to order
        ///</summary>
        ///param name="cartDetails"></param>
        ///<param name="authId"></param>
        Guid UpdateOrderIdToCart(List<Cart> cartDetails, Guid authId);
    }
}
