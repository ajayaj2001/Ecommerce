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
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        //private readonly IProductService _productService;
        private readonly ICartRepository _cartRepository;
        //private readonly IProductRepository _productRepository;


        public CartService(IMapper mapper, ICartRepository cartRepository)//, IProductService ProductService,IProductRepository productRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_productService = ProductService ?? throw new ArgumentNullException(nameof(ProductService));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            //_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
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
        public List<ReturnCartDto> GetCartForUser(Guid userId)
        {
            List<ReturnCartDto> resultCartDetails = new List<ReturnCartDto>();

            IEnumerable<Cart> cartDetails = _cartRepository.GetCartDetailsForUser(userId);

            for (int i = 0; i < cartDetails.Count(); i++)
            {
                Cart cartDetail = cartDetails.ElementAt(i);
                ReturnCartDto resultCart = new ReturnCartDto();
                //resultCart.Product = _mapper.Map<ResultProductDto>(_productService.GetDetailedProductById(cartDetail.ProductId));
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
        public Guid UpdateOrderIdToCart(List<Cart> cartDetails, Guid authId)
        {
            Guid orderId = Guid.NewGuid();
            for (int i = 0; i < cartDetails.Count; i++)
            {
                Cart cartDetail = cartDetails[i];
                //update product detail
               /* ProductDetail product = _productService.GetProductById(cartDetail.ProductId);
                product.Quantity -= cartDetail.Quantity;
                _productRepository.UpdateProduct(product);
                _productRepository.Save();*/
                //update card detail
                cartDetail.OrderId = orderId;
                cartDetail.IsActive = false;
                _cartRepository.UpdateCartProduct(cartDetail);
                _cartRepository.Save();
            }
            return orderId;
        }
    }
}
