using AutoMapper;
using Microsoft.Extensions.Logging;
using Order.Contracts.Repositories;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using Order.Entities.Models;
using Product.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Services
{
    public class WishListService : IWishListService
    {
        private readonly IMapper _mapper;
        //private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IWishListRepository _wishListRepository;

        public WishListService(IMapper mapper, IWishListRepository wishListRepository, ICartService cartService)//, IProductService ProductService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_productService = ProductService ?? throw new ArgumentNullException(nameof(ProductService));
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
        ///check if product already added to datbase
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
        ///param name="authId"></param>
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
        ///param name="authId"></param>
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
        ///param name="authId"></param>
        public FetchWishListDto GetWishListByName(string wishlistName, Guid authId)
        {
            FetchWishListDto resultWishList = new FetchWishListDto();
            IEnumerable<WishList> wishListFromRepo = _wishListRepository.GetWishlistByName(wishlistName, authId);

            resultWishList.Name = wishlistName;
            for (int i = 0; i < wishListFromRepo.Count(); i++)
            {
                /*ProductDto productDetail = _productService.GetDetailedProductById(wishListFromRepo.ElementAt(i).ProductId);
                if (productDetail != null && productDetail.Visibility)
                {
                    resultWishList.ProductList.Add(_mapper.Map<ResultProductDto>(productDetail));
                }*/
            }
            return resultWishList;
        }

        ///<summary>
        ///fetch wishlist in database
        ///</summary>
        ///<param name="wishlistName"></param>
        ///param name="authId"></param>
        public void MoveWishListToCart(string wishlistName, Guid authId)
        {
            IEnumerable<WishList> wishListFromRepo = _wishListRepository.GetWishlistByName(wishlistName, authId);

            for (int i = 0; i < wishListFromRepo.Count(); i++)
            {
                _cartService.AddToCart(new CreateCartDto() { Quantity = 1, ProductId = wishListFromRepo.ElementAt(i).ProductId }, authId);
            }
            DeleteWishlistByName(wishlistName, authId);
        }

        ///<summary>
        ///fetch wishlist in database
        ///</summary>
        ///param name="userId"></param>
        public List<FetchWishListDto> GetWishListForUser(Guid userId)
        {
            List<FetchWishListDto> resultWishList = new List<FetchWishListDto>();

            IEnumerable<string> wishlistNames = _wishListRepository.GetWishlistNameForUser(userId);

            for (int i = 0; i < wishlistNames.Count(); i++)
            {
                resultWishList.Add(GetWishListByName(wishlistNames.ElementAt(i), userId));
            }
            return resultWishList;

        }
    }
}
