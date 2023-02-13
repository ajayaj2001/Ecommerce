using AutoMapper;
using Newtonsoft.Json.Linq;
using Order.Contracts.Repositories;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IApiService _apiService;
        private readonly ICartRepository _cartRepository;


        public CartService(IMapper mapper, ICartRepository cartRepository, IApiService apiService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        ///<summary>
        ///create new wishlist in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="cartDetail"></param>
        public Guid AddToCart(CreateCartDto cartDetail, Guid authId)
        {
            Cart cart = _mapper.Map<Cart>(cartDetail);
            if (checkProductExist(cartDetail.ProductId, authId))
            {
                UpdateCartProduct(cartDetail, authId);
            }
            cart.UserId = authId;
            cart.CreatedAt = DateTime.Now.ToString();
            cart.CreatedBy = authId;
            _cartRepository.AddProductToCart(cart);
            _cartRepository.Save();

            return cart.Id;
        }

        ///<summary>
        ///fetch cart in database
        ///</summary>
        ///param name="userId"></param>
        public List<ReturnCartDto> GetCartForUser(Guid userId, string token)
        {
            List<ReturnCartDto> resultCartDetails = new List<ReturnCartDto>();

            IEnumerable<Cart> cartDetails = _cartRepository.GetCartDetailsForUser(userId);
            List<Guid> ids = new List<Guid>();
            for (int i = 0; i < cartDetails.Count(); i++)
            {
                ids.Add(cartDetails.ElementAt(i).ProductId);
            }
            List<ResultProductDto> productList = _apiService.GetProductByIds(ids, token);

            for (int i = 0; i < cartDetails.Count(); i++)
            {
                Cart cartDetail = cartDetails.ElementAt(i);
                ReturnCartDto resultCart = new ReturnCartDto();
                resultCart.Product = productList.Find(s => s.Id == cartDetail.ProductId);
                resultCart.Quantity = cartDetail.Quantity;
                resultCartDetails.Add(resultCart);
            }

            return resultCartDetails;
        }

        ///<summary>
        ///check if wishlist name already added to datbase
        ///</summary>
        ///<param name="name"></param>
        ///<param name="userId"></param>
        public bool checkProductExist(Guid productId, Guid userId)
        {
            return _cartRepository.CheckProductExistInCart(productId, userId);
        }

        ///<summary>
        ///delete wishlist in database
        ///</summary>
        ///<param name="productId"></param>
        ///param name="authId"></param>
        public void DeleteCartProduct(Guid productId, Guid authId)
        {
            Cart cartDetail = _cartRepository.GetProductFromCart(productId, authId);
            cartDetail.IsActive = false;
            _cartRepository.Save();
        }

        ///<summary>
        ///update product on cart
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="cartInput"></param>
        public void UpdateCartProduct(CreateCartDto cartInput, Guid authId)
        {
            Cart cart = _cartRepository.GetProductFromCart(cartInput.ProductId, authId);
            _mapper.Map(cartInput, cart);
            cart.UpdatedAt = DateTime.Now.ToString();
            cart.UpdatedBy = authId;
            _cartRepository.UpdateCartProduct(cart);
            _cartRepository.Save();
        }

        ///<summary>
        ///fetch cart in database
        ///</summary>
        ///param name="userId"></param>
        public IEnumerable<Cart> GetCartDetails(Guid userId)
        {
            IEnumerable<Cart> cartDetails = _cartRepository.GetCartDetailsForUser(userId);
            return cartDetails;
        }

        ///<summary>
        ///move cart to order
        ///</summary>
        ///param name="cartDetails"></param>
        ///<param name="authId"></param>
        public string UpdateOrderIdToCart(List<Cart> cartDetails, Guid authId, string token)
        {
            Guid orderId = Guid.NewGuid();

            List<UpdateProductQuantityDto> productList = new List<UpdateProductQuantityDto>();
            for (int i = 0; i < cartDetails.Count; i++)
            {
                Cart cartDetail = cartDetails.ElementAt(i);
                productList.Add(new UpdateProductQuantityDto() { Id = cartDetail.ProductId, Quantity = cartDetail.Quantity });
            }

            bool isProductUpdated = _apiService.UpdateProductByIds(productList, token);
            if (!isProductUpdated)
                return "failed";
            for (int i = 0; i < cartDetails.Count; i++)
            {
                Cart cartDetail = cartDetails[i];
                //update card detail
                cartDetail.OrderId = orderId;
                cartDetail.IsActive = false;
                _cartRepository.UpdateCartProduct(cartDetail);
                _cartRepository.Save();
            }
            return orderId.ToString();
        }
    }
}
