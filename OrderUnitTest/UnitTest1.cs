using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Entities.Dtos;
using Order.Contracts.Repositories;
using Order.Contracts.Services;
using Order.Controllers;
using Order.DbContexts;
using Order.Repositories;
using Order.Services;
using ProductUnitTest.InMemoryContext;
using System;
using System.Security.Claims;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace OrderUnitTest
{
    public class UnitTest1
    {

        public readonly WishListController _wishListController;
        public readonly CartController _cartController;
        public readonly IWishListService _wishListService;
        public readonly ICartService _cartService;
        private readonly IWishListRepository _wishListRepository;
        private readonly ICartRepository _cartRepository;
        private readonly OrderContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IHttpClientWrapperService _httpClientWrapperService;

        public UnitTest1()
        {
            _configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            using ServiceProvider services = new ServiceCollection()
                            .AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(_configuration)
                            // -> add your DI needs here
                            .BuildServiceProvider();

            _context = InMemorydbContext.orderContext();
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder().
           ConfigureLogging((builderContext, loggingBuilder) =>
           {
               loggingBuilder.AddConsole((options) =>
               {
                   options.IncludeScopes = true;
               });
           });
            IHost host = hostBuilder.Build();
            _logger = host.Services.GetRequiredService<ILogger<WishListController>>();

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Order.Profiles.Mapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _cartRepository = new CartRepository(_context);
            _wishListRepository = new WishListRepository(_context);

            string userId = "5bfdfa9f-ffa2-4c31-40de-08db05cf468e";
            Mock<IHttpClientWrapperService> mockClient = new Mock<IHttpClientWrapperService>();
            mockClient.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(productIdTask);
            mockClient.Setup(x => x.GetProducts(It.IsAny<string>(), It.IsAny<List<Guid>>())).Returns(productIdsTask);

            mockClient.Setup(x => x.GetOrderDetail(It.IsAny<string>(), It.IsAny<CreateOrderDto>())).Returns(Task.FromResult(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                }));

            mockClient.Setup(x => x.UpdateProducts(It.IsAny<string>(), It.IsAny<List<UpdateProductQuantityDto>>())).Returns(Task.FromResult(
               new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
               }));

            Task<ResultProductDto> productIdTask()
            {
                return Task.Run(() =>
                {
                    return new ResultProductDto()
                    {
                        Id = Guid.Parse(userId),
                        Name = "Fruit",
                        Quantity = 50,
                        Description = "fresh fruit",
                        Type = "Fruit"
                    };
                });
            };

            Task<HttpResponseMessage> productIdsTask()
            {
                return Task.Run(() =>
                {
                    HttpResponseMessage response = new HttpResponseMessage();

                    List<ResultProductDto> products = new List<ResultProductDto>();

                    products.Add(new ResultProductDto()
                    {
                        Id = Guid.Parse(userId),
                        Name = "Fruit",
                        Quantity = 50,
                        Description = "fresh fruit",
                        Type = "Fruit"
                    });
                    response.Content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json");

                    return response;
                });

            };

            _cartService = new CartService(_mapper, _cartRepository, _configuration, mockClient.Object);
            _wishListService = new WishListService(_mapper, _wishListRepository, _cartService);
            _cartController = new CartController(_logger, _cartService);
            _wishListController = new WishListController(_logger, _wishListService, _cartService);

            Mock<HttpContext> contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier,userId),
                                        new Claim(ClaimTypes.Role,"admin")
                                        // other required and custom claims
                           }, "TestAuthentication")));
            _cartController.ControllerContext.HttpContext = contextMock.Object;
            _wishListController.ControllerContext.HttpContext = contextMock.Object;
        }

        public void RestoreContext()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        /// <summary>
        ///   To test get wish list by wishlist name
        /// </summary>
        [Fact]
        public void GetWishlistByName_OkObjectResult()
        {
            string wishlistName = "Personal";
            ActionResult response = _wishListController.GetWishlistByName(wishlistName) as ActionResult;
            Assert.IsType<OkObjectResult>(response);
            RestoreContext();
        }

        /// <summary>
        ///   To test get wishlist by invalid wishlist name
        /// </summary>
        [Fact]
        public void GetWishlistByName_NotFoundObjectResult()
        {
            string wishlistName = "Home";
            ActionResult response = _wishListController.GetWishlistByName(wishlistName) as ActionResult;
            Assert.IsType<NotFoundObjectResult>(response);
            RestoreContext();
        }

        /// <summary>
        ///   To test get all wishlist
        /// </summary>
        [Fact]
        public void GetWishlist_OkObjectResult()
        {
            ActionResult response = _wishListController.GetWishlist() as ActionResult;
            Assert.IsType<OkObjectResult>(response);
            RestoreContext();
        }

        /// <summary>
        ///   To test create new wishlist
        /// </summary>
        [Fact]
        public void CreateWishlist_OkObjectResult()
        {
            CreateWishListDto wishList = new CreateWishListDto()
            {
                Name = "Home",
                ProductId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e")
            };
            ActionResult<string> response = _wishListController.CreateWishList(wishList);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test create new wishlist by existing detail
        /// </summary>
        [Fact]
        public void CreateWishlist_ConflitObjectResult()
        {
            CreateWishListDto wishList = new CreateWishListDto()
            {
                Name = "Personal",
                ProductId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e")
            };
            ActionResult<string> response = _wishListController.CreateWishList(wishList);
            Assert.IsType<ConflictObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test delete wishlist product by product id
        /// </summary>
        [Fact]
        public void DeleteWishlistProduct_OkObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            string wishlistName = "Personal";
            ActionResult<string> response = _wishListController.DeleteWishlistProduct(new CreateWishListDto() { Name = wishlistName, ProductId = productId });
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test delete wishlist product by invalid product id
        /// </summary>
        [Fact]
        public void DeleteWishlistProduct_NotFoundObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4687");
            string wishlistName = "Personal";
            ActionResult<string> response = _wishListController.DeleteWishlistProduct(new CreateWishListDto() { Name = wishlistName, ProductId = productId });
            Assert.IsType<NotFoundObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test delete wishlist by wishlist name
        /// </summary>
        [Fact]
        public void DeleteWishlist_OkObjectResult()
        {
            string wishlistName = "Personal";
            ActionResult<string> response = _wishListController.DeleteWishlist(wishlistName);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test delete wishlist by non existing wishlist name
        /// </summary>
        [Fact]
        public void DeleteWishlist_NotFoundObjectResult()
        {
            string wishlistName = "Home";
            ActionResult<string> response = _wishListController.DeleteWishlist(wishlistName);
            Assert.IsType<NotFoundObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test move non exist wishlist to cart
        /// </summary>
        [Fact]
        public void MoveWishListToCart_NotFoundObjectResult()
        {
            string wishlistName = "Home";
            ActionResult<string> response = _wishListController.MoveWishListToCart(wishlistName);
            Assert.IsType<NotFoundObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test move wishlist to cart
        /// </summary>
        [Fact]
        public void MoveWishListToCart_OkObjectResult()
        {
            string wishlistName = "Personal";
            ActionResult<string> response = _wishListController.MoveWishListToCart(wishlistName);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        //cart

        /// <summary>
        ///   To test create cart
        /// </summary>
        [Fact]
        public void CreateCart_OkObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4677");
            CreateCartDto cart = new CreateCartDto() { ProductId = productId, Quantity = 30 };
            ActionResult<string> response = _cartController.AddToCart(cart);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test get all cart
        /// </summary>
        [Fact]
        public void GetCart_OkObjectResult()
        {
            ActionResult<string> response = _cartController.GetCart();
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test update cart
        /// </summary>
        [Fact]
        public void UpdateCart_OkObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            CreateCartDto cart = new CreateCartDto() { ProductId = productId, Quantity = 40 };
            ActionResult<string> response = _cartController.UpdateCart(cart);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test update cart by out of stock product
        /// </summary>
        [Fact]
        public void UpdateCart_BadRequestObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            CreateCartDto cart = new CreateCartDto() { ProductId = productId, Quantity = 100 };
            ActionResult<string> response = _cartController.UpdateCart(cart);
            Assert.IsType<BadRequestObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test update cart by non existing product
        /// </summary>
        [Fact]
        public void UpdateCart_NotFoundRequestObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4633");
            CreateCartDto cart = new CreateCartDto() { ProductId = productId, Quantity = 20 };
            ActionResult<string> response = _cartController.UpdateCart(cart);
            Assert.IsType<NotFoundObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test delete cart product by product id
        /// </summary>
        [Fact]
        public void DeleteCart_OkObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            ActionResult<string> response = _cartController.DeleteCartProduct(productId);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test delete cart product by non exist product id
        /// </summary>
        [Fact]
        public void DeleteCart_NotFoundObjectResult()
        {
            Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4677");
            ActionResult<string> response = _cartController.DeleteCartProduct(productId);
            Assert.IsType<NotFoundObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test move cart to order 
        /// </summary>
        [Fact]
        public void CartToOrder_OkObjectResult()
        {
            Guid cardId = Guid.Parse("dbe63d8c-7a58-4c10-a68f-4e7a903ef5a8");
            Guid addressId = Guid.Parse("7cf56f52-1aab-4646-b090-d337aac18370");
            CreateOrderDto createOrder = new CreateOrderDto() { CardId = cardId, AddressId = addressId };
            ActionResult<string> response = _cartController.MoveToOrder(createOrder);
            Assert.IsType<OkObjectResult>(response.Result);
            RestoreContext();
        }

        /// <summary>
        ///   To test move cart to order by empty cart
        /// </summary>
        [Fact]
        public void CartToOrder_NotFoundObjectResult()
        {
            _context.Database.EnsureDeleted();
            Guid cardId = Guid.Parse("dbe63d8c-7a58-4c10-a68f-4e7a903ef5a8");
            Guid addressId = Guid.Parse("7cf56f52-1aab-4646-b090-d337aac18370");
            CreateOrderDto createOrder = new CreateOrderDto() { CardId = cardId, AddressId = addressId };
            ActionResult<string> response = _cartController.MoveToOrder(createOrder);
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

    }
}
