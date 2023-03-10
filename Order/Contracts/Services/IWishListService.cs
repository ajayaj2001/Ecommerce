using Order.Entities.Dtos;
using System;
using System.Collections.Generic;

namespace Order.Contracts.Services
{
    public interface IWishListService
    {
        ///<summary>
        ///create new wishlist in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="wishListInput"></param>
        Guid CreateWishList(CreateWishListDto wishListInput, Guid authId);

        ///<summary>
        ///check if product already added to datbase
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="wishList"></param>
        bool checkIfAlreadyExist(CreateWishListDto wishList, Guid userId);

        ///<summary>
        ///check if wishlist name already added to datbase
        ///</summary>
        ///<param name="name"></param>
        ///<param name="userId"></param>
        bool checkWishListExist(string name, Guid userId);

        ///<summary>
        ///delete wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
         void DeleteWishlistByName(string wishlistName, Guid authId);

        ///<summary>
        ///delete wishlist product in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        void DeleteWishlistProduct(CreateWishListDto wishlistName, Guid authId);

        ///<summary>
        ///fetch wishlist by wishlist name in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        ///<param name="token"></param>
        FetchWishListDto GetWishListByName(string wishlistName, Guid authId);

        ///<summary>
        ///fetch wishlist for user in database
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="token"></param>
        List<FetchWishListDto> GetWishListForUser(Guid userId);

        ///<summary>
        ///move wishlist to cart
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        void MoveWishListToCart(string wishlistName, Guid authId);
    }
}
