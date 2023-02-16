using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Entities.Dtos;
using Product.Contracts.Repositories;
using Product.Contracts.Services;
using Product.Controllers;
using Product.DbContexts;
using Product.Entities.Dtos;
using Product.Repositories;
using Product.Services;
using ProductUnitTest.InMemoryContext;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace CustomerUnitTest
{
    public class UnitTest1
    {
        public readonly ProductController _productController;
        public readonly IProductService _productService;
        public readonly IProductRepository _productRepository;
        public readonly ICategoryRepository _categoryRepository;
        private readonly ProductContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UnitTest1()
        {
            _configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            using ServiceProvider services = new ServiceCollection()
                            .AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(_configuration)
                            // -> add your DI needs here
                            .BuildServiceProvider();

            _context = InMemorydbContext.productContext();
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder().
           ConfigureLogging((builderContext, loggingBuilder) =>
           {
               loggingBuilder.AddConsole((options) =>
               {
                   options.IncludeScopes = true;
               });
           });
            IHost host = hostBuilder.Build();
            _logger = host.Services.GetRequiredService<ILogger<ProductController>>();

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Product.Profiles.Mapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _productRepository = new ProductRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _productService = new ProductService(_mapper, _productRepository, _categoryRepository);
            _productController = new ProductController(_mapper, _logger, _productService);

            string userId = "5bfdfa9f-ffa2-4c31-40de-08db05cf468e";
            Mock<HttpContext> contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier,userId),
                                        new Claim(ClaimTypes.Role,"admin")
                                        // other required and custom claims
                           }, "TestAuthentication")));
            _productController.ControllerContext.HttpContext = contextMock.Object;
        }

        Guid productId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");

        /// <summary>
        ///   To test get product details by product id
        /// </summary>
        [Fact]
        public void GetProductById_OkObjectResult()
        {
            ActionResult response = _productController.GetProductById(productId) as ActionResult;
            Assert.IsType<OkObjectResult>(response);
        }

        /// <summary>
        ///   To test get product details by invalid product id
        /// </summary>
        [Fact]
        public void GetProductById_NotFoundObjectResult()
        {
            Guid userId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4684");
            ActionResult response = _productController.GetProductById(userId) as ActionResult;
            Assert.IsType<NotFoundObjectResult>(response);
        }

        /// <summary>
        ///   To test create new product with valid product details 
        /// </summary>
        [Fact]
        public void CreateProduct_OkObjectResult()
        {
            CreateProductDto product = new CreateProductDto()
            {
                Name = "apple",
                Description = "a healthy apple from fresh farm",
                Quantity = 30,
                Visibility = true,
                Type = "Fruit",
            };
            ActionResult<string> response = _productController.CreateProduct(product);
            Assert.IsType<OkObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test create new product with invalid product details 
        /// </summary>
        [Fact]
        public void CreateProduct_NotFoundObjectResult()
        {
            CreateProductDto product = new CreateProductDto()
            {
                Name = "Briyani",
                Description = "a stunnig tasty briyani",
                Quantity = 30,
                Visibility = true,
                Type = "Food",
            };
            ActionResult<string> response = _productController.CreateProduct(product);
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test the get all product by filter
        /// </summary>
        [Fact]
        public void GetAllProduct_OkObjectResult()
        {
            ActionResult response1 = _productController.GetAllProduct(1, 1) as ActionResult;
            Assert.IsType<OkObjectResult>(response1);
            OkObjectResult item = response1 as OkObjectResult;
            Assert.IsType<List<ResultProductDto>>(item.Value);
        }

        /// <summary>
        ///  To test the get all product with wrong category
        /// </summary>
        [Fact]
        public void GetAllProduct_NotFoundObjectResult()
        {
            ActionResult response1 = _productController.GetAllProduct(1, 1, "Name", "ASC", "Food") as ActionResult;
            Assert.IsType<NotFoundObjectResult>(response1);
        }

        /// <summary>
        ///  To test update product with product details 
        /// </summary>
        [Fact]
        public void UpdateProduct_OkObjectResult()
        {
            UpdateProductDto product = new UpdateProductDto()
            {
                Name = "apple",
                Description = "a healthy apple from fresh farm",
                Quantity = 30,
                Visibility = true,
                Type = "Fruit",
            };
            ActionResult<string> response = _productController.UpdateProduct(productId, product);
            Assert.IsType<OkObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test update product with invalid product details 
        /// </summary>
        [Fact]
        public void UpdateUser_NotFoundObjectResult()
        {
            UpdateProductDto product = new UpdateProductDto()
            {
                Name = "apple",
                Description = "a healthy apple from fresh farm",
                Quantity = 30,
                Visibility = true,
                Type = "Fruit",
            };
            Guid product2 = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4685");
            ActionResult<string> response = _productController.UpdateProduct(product2, product);
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }


        /// <summary>
        ///  To test get product list by id list
        /// </summary>
        [Fact]
        public void GetProductByIdsList_OkObjectResult()
        {
            List<Guid> guids= new List<Guid>();
            guids.Add(productId);
            ActionResult response = _productController.GetProductByIdsService(guids);
            Assert.IsType<OkObjectResult>(response);
        }

        /// <summary>
        ///  To test update products by list with id and quantity 
        /// </summary>
        [Fact]
        public void UpdateProductByList_OkObjectResult()
        {
            List<UpdateProductQuantityDto> productQuantitylist = new List<UpdateProductQuantityDto>();
            productQuantitylist.Add(new UpdateProductQuantityDto() { Id=productId,Quantity=10});
            ActionResult response = _productController.UpdateProductByIdsService(productQuantitylist);
            Assert.IsType<OkResult>(response);
        }

        /// <summary>
        ///  To test delete product with non existing product id 
        /// </summary>
        [Fact]
        public void DeleteProduct_NotFoundObjectResult()
        {
            Guid product2 = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4685");
            ActionResult response = _productController.DeleteProduct(product2);
            Assert.IsType<NotFoundObjectResult>(response);
        }

        /// <summary>
        ///  To test delete product with product id
        /// </summary>
        [Fact]
        public void DeleteProduct_OkObjectResult()
        {
            ActionResult response = _productController.DeleteProduct(productId);
            Assert.IsType<OkObjectResult>(response);
        }

    }
}
