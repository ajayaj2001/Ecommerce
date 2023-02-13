using Order.Entities.Models;
using System.Collections.Generic;
using System;

namespace Order.Contracts.Repositories
{
    public interface ICartRepository
    {
        ///<summary>
        ///add product to cart
        ///</summary>
        ///<param name="cart"></param>
        void AddProductToCart(Cart cart);

        ///<summary>
        ///get cart for user
        ///</summary>
        ///param name="authId"></param>
        IEnumerable<Cart> GetCartDetailsForUser(Guid authId);

        ///<summary>
        ///check if proudct name already exist in cart
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
        bool CheckProductExistInCart(Guid productId, Guid userId);

        ///<summary>
        ///get cart product by id
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
         Cart GetProductFromCart(Guid productId, Guid userId);

        ///<summary>
        ///update product in cart
        ///</summary>
        ///<param name="cart"></param>
         void UpdateCartProduct(Cart cart);

        ///<summary>
        ///save all changes
        ///</summary>
        bool Save();
    }
}
