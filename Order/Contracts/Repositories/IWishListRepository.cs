using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;

namespace Order.Contracts.Repositories
{
    public interface IWishListRepository
    {
        ///<summary>
        ///to create new wishlist for user in db
        ///</summary>
        ///<param name="wishList"></param>
        void CreateWishList(WishList wishList);

        ///<summary>
        ///check product already exist in wishlist
        ///</summary>
        ///<param name="wishList"></param>
        ///<param name="userId"></param>
        bool checkIfProductExist(CreateWishListDto wishList, Guid userId);

        ///<summary>
        ///check if wishlist name already added to datbase
        ///</summary>
        ///<param name="name"></param>
        ///<param name="userId"></param>
        bool checkWishListExist(string name, Guid userId);

        ///<summary>
        ///get wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        List<WishList> GetWishlistByName(string wishlistName, Guid authId);

        ///<summary>
        ///get wishlist product in database
        ///</summary>
        ///<param name="wishlist"></param>
        ///<param name="authId"></param>
        WishList GetWishlistProduct(CreateWishListDto wishlist, Guid authId);

        ///<summary>
        ///get wishlist name in database
        ///</summary>
        ///<param name="authId"></param>
         List<string> GetWishlistNameForUser(Guid authId);

        ///<summary>
        ///save all changes
        ///</summary>
        bool Save(Guid authId);
    }
}
