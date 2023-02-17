using AutoMapper;
using Order.Contracts.Repositories;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Services
{
    public class WishListService : IWishListService
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IWishListRepository _wishListRepository;

        public WishListService(IMapper mapper, IWishListRepository wishListRepository, ICartService cartService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(CartService));
            _wishListRepository = wishListRepository ?? throw new ArgumentNullException(nameof(wishListRepository));
        }

        ///<summary>
        ///create new wishlist in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="wishListInput"></param>
        public Guid CreateWishList(CreateWishListDto wishListInput, Guid authId)
        {
            WishList wishList = _mapper.Map<WishList>(wishListInput);
            wishList.UserId = authId;
            wishList.CreatedAt = DateTime.Now.ToString();
            wishList.CreatedBy = authId;
            _wishListRepository.CreateWishList(wishList);
            _wishListRepository.Save();
            return wishList.Id;
        }

        ///<summary>
        ///check if product already added to wishlist
        ///</summary>
        ///<param name="wishList"></param>
        ///<param name="userId"></param>
        public bool checkIfAlreadyExist(CreateWishListDto wishList, Guid userId)
        {
            return _wishListRepository.checkIfProductExist(wishList, userId);
        }

        ///<summary>
        ///delete wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        public void DeleteWishlistByName(string wishlistName, Guid authId)
        {
            IEnumerable<WishList> wishListFromRepo = _wishListRepository.GetWishlistByName(wishlistName, authId);
            foreach (WishList list in wishListFromRepo)
            {
                list.IsActive = false;
            }
            _wishListRepository.Save();
        }

        ///<summary>
        ///delete wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        public void DeleteWishlistProduct(CreateWishListDto wishlistName, Guid authId)
        {
            WishList wishListFromRepo = _wishListRepository.GetWishlistProduct(wishlistName, authId);
            wishListFromRepo.IsActive = false;
            _wishListRepository.Save();
        }

        ///<summary>
        ///check if wishlist name already added to datbase
        ///</summary>
        ///<param name="name"></param>
        ///<param name="userId"></param>
        public bool checkWishListExist(string name, Guid userId)
        {
            return _wishListRepository.checkWishListExist(name, userId);
        }

        ///<summary>
        ///fetch wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        ///<param name="token"></param>
        public FetchWishListDto GetWishListByName(string wishlistName, Guid authId, string token)
        {
            FetchWishListDto resultWishList = new FetchWishListDto();
            IEnumerable<WishList> wishListFromRepo = _wishListRepository.GetWishlistByName(wishlistName, authId);
            List<Guid> ids = new List<Guid>();
            resultWishList.Name = wishlistName;
            foreach (WishList wishlist in wishListFromRepo)
            {
                ids.Add(wishlist.ProductId);
            }
            resultWishList.ProductList = _cartService.GetProductByIds(ids, token);
            return resultWishList;
        }

        ///<summary>
        ///move wishlist product to cart
        ///</summary>
        ///<param name="wishlistName"></param>
        ///<param name="authId"></param>
        public void MoveWishListToCart(string wishlistName, Guid authId)
        {
            IEnumerable<WishList> wishListFromRepo = _wishListRepository.GetWishlistByName(wishlistName, authId);
            foreach (WishList wishList in wishListFromRepo)
            {
                _cartService.AddToCart(new CreateCartDto() { Quantity = 1, ProductId = wishList.ProductId }, authId);
            }
            DeleteWishlistByName(wishlistName, authId);
        }

        ///<summary>
        ///fetch wishlist in database
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="token"></param>
        public List<FetchWishListDto> GetWishListForUser(Guid userId, string token)
        {
            List<FetchWishListDto> resultWishList = new List<FetchWishListDto>();
            IEnumerable<string> wishlistNames = _wishListRepository.GetWishlistNameForUser(userId);
            foreach (string wishListName in wishlistNames)
            {
                resultWishList.Add(GetWishListByName(wishListName, userId, token));
            }
            return resultWishList;
        }
    }
}
