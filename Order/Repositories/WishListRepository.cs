using Microsoft.EntityFrameworkCore.Internal;
using Order.Contracts.Repositories;
using Order.DbContexts;
using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly OrderContext _context;
        public WishListRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="wishList"></param>
        public void CreateWishList(WishList wishList)
        {
            _context.WishLists.Add(wishList);
        }

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="wishList"></param>
        ///param name="userId"></param>
        public bool checkIfProductExist(CreateWishListDto wishList, Guid userId)
        {
            return _context.WishLists.Any(e => e.UserId == userId && e.Name == wishList.Name && e.ProductId == wishList.ProductId && e.IsActive);
        }

        ///<summary>
        ///check if wishlist name already added to datbase
        ///</summary>
        ///<param name="name"></param>
        ///<param name="userId"></param>
        public bool checkWishListExist(string name, Guid userId)
        {
            return _context.WishLists.Any(e => e.UserId == userId && e.Name == name && e.IsActive);
        }

        //<summary>
        ///get wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///param name="authId"></param>
        public IEnumerable<WishList> GetWishlistByName(string wishlistName, Guid authId)
        {
            return _context.WishLists.Where(list => list.Name == wishlistName && list.UserId == authId && list.IsActive);
        }

        //<summary>
        ///get wishlist product in database
        ///</summary>
        ///<param name="wishlist"></param>
        ///param name="authId"></param>
        public WishList GetWishlistProduct(CreateWishListDto wishlist, Guid authId)
        {
            return _context.WishLists.FirstOrDefault(list => list.Name == wishlist.Name && list.ProductId == wishlist.ProductId && list.UserId == authId);
        }

        //<summary>
        ///get wishlist name in database
        ///</summary>
        ///param name="authId"></param>
        public IEnumerable<string> GetWishlistNameForUser(Guid authId)
        {
            return _context.WishLists.Where(x=>x.UserId==authId&&x.IsActive).Select(x=>x.Name).Distinct();
        }

        ///<summary>
        ///save all changes
        ///</summary>
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
