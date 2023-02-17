using AutoMapper;
using Order.Contracts.Repositories;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Order.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        public IConfiguration _configuration;


        public CartService(IMapper mapper, ICartRepository cartRepository, IConfiguration configuration)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        ///<summary>
        ///add to cart
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
        ///<param name="userId"></param>
        public List<ReturnCartDto> GetCartForUser(Guid userId, string token)
        {
            List<ReturnCartDto> resultCartDetails = new List<ReturnCartDto>();

            IEnumerable<Cart> cartDetails = _cartRepository.GetCartDetailsForUser(userId);
            foreach (var cart in cartDetails)
            {

            }
            List<Guid> ids = new List<Guid>();

            foreach (Cart cart in cartDetails)
            {
                ids.Add(cart.ProductId);
            }
            List<ResultProductDto> productList = GetProductByIds(ids, token);
            foreach (Cart cart in cartDetails)
            {
                ResultProductDto Product = productList.Find(s => s.Id == cart.ProductId);
                if (Product != null)
                {
                    ReturnCartDto resultCart = new ReturnCartDto();
                    resultCart.Product = Product;
                    resultCart.Quantity = cart.Quantity;
                    resultCartDetails.Add(resultCart);
                }
            }
            return resultCartDetails;
        }

        ///<summary>
        ///check product exist in cart
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
        public bool checkProductExist(Guid productId, Guid userId)
        {
            return _cartRepository.CheckProductExistInCart(productId, userId);
        }

        ///<summary>
        ///delete cart in database
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="authId"></param>
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
        ///<param name="userId"></param>
        public IEnumerable<Cart> GetCartDetails(Guid userId)
        {
            IEnumerable<Cart> cartDetails = _cartRepository.GetCartDetailsForUser(userId);
            return cartDetails;
        }

        ///<summary>
        ///move cart to order
        ///</summary>
        ///<param name="cartDetails"></param>
        ///<param name="authId"></param>
        public string UpdateOrderIdToCart(List<Cart> cartDetails, Guid authId, string token)
        {
            Guid orderId = Guid.NewGuid();
            List<UpdateProductQuantityDto> productList = new List<UpdateProductQuantityDto>();
            foreach (Cart cart in cartDetails)
            {
                productList.Add(new UpdateProductQuantityDto() { Id = cart.ProductId, Quantity = cart.Quantity });
            }

            bool isProductUpdated = UpdateProductByIds(productList, token);
            if (!isProductUpdated)
                return "failed";

            foreach (Cart cart in cartDetails)
            {
                cart.OrderId = orderId;
                cart.IsActive = false;
                _cartRepository.UpdateCartProduct(cart);
                _cartRepository.Save();
            }
            return orderId.ToString();
        }

        ///<summary>
        ///update product by ids
        ///</summary>
        ///<param name="productDetails"></param>
        ///<param name="token"></param>
        public bool UpdateProductByIds(List<UpdateProductQuantityDto> productDetails, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.PutAsJsonAsync<List<UpdateProductQuantityDto>>("api/product/updateproducts", productDetails).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        ///<summary>
        ///get single product by id
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="token"></param>
        public ResultProductDto GetProductById(Guid productId, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                ResultProductDto response = client.GetFromJsonAsync<ResultProductDto>($"api/product/{productId}").Result;
                return response;
            }
            catch (Exception ex)//404 exception handling
            {
                return null;
            }
        }

        ///<summary>
        ///get product by ids
        ///</summary>
        ///<param name="productIds"></param>
        ///<param name="token"></param>
        public List<ResultProductDto> GetProductByIds(List<Guid> productIds, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.PostAsJsonAsync<List<Guid>>("api/product/getproducts", productIds).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content.ReadFromJsonAsync<List<ResultProductDto>>().Result;
            else
                return Enumerable.Empty<ResultProductDto>().ToList();
        }

    }
}
