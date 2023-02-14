using AutoMapper;
using Customer.Contracts.Repositories;
using Customer.Contracts.Services;
using Customer.Controllers;
using Customer.DbContexts;
using Customer.Entities.Dtos;
using Customer.Entities.Models;
using Customer.Migrations;
using Customer.Repositories;
using Customer.Services;
using JWTAuthenticationManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace CustomerUnitTest
{
    public class UnitTest1
    {
        public readonly UserController _userController;
        public readonly AuthController _authController;
        public readonly IUserService _userService;
        public readonly IAuthService _authService;
        public readonly IUserRepository _userRepository;
        private readonly CustomerContext _context;
        private readonly IConfiguration _configuration;
        private readonly JWTTokenHandler _jwtTokenHandler;
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

            _context = InMemoryContext.InMemorydbContext.customerContext();
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder().
           ConfigureLogging((builderContext, loggingBuilder) =>
           {
               loggingBuilder.AddConsole((options) =>
               {
                   options.IncludeScopes = true;
               });
           });
            IHost host = hostBuilder.Build();
            _logger = host.Services.GetRequiredService<ILogger<UserController>>();

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Customer.Profiles.Mapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _userRepository = new UserRepository(_context);
            _authService = new AuthService(_userRepository);
            _userService = new UserService(_mapper, _userRepository, _logger);
            _userController = new UserController(_logger, _userService);
            _authController = new AuthController(_logger, _authService, _jwtTokenHandler);
            _jwtTokenHandler = new JWTTokenHandler();
            string userId = "5bfdfa9f-ffa2-4c31-40de-08db05cf468e";
            Mock<HttpContext> contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier,userId)
                                        // other required and custom claims
                           }, "TestAuthentication")));
            _userController.ControllerContext.HttpContext = contextMock.Object;
            _authController.ControllerContext.HttpContext = contextMock.Object;
        }



        /// <summary>
        ///   To test address book count 
        /// </summary>
        [Fact]
        public void CreateUser_OkObjectResult()
        {

            CreateUserDto user = new CreateUserDto()
            {
                FirstName = "",
                LastName = "",
                EmailAddress = "",
            };

            user.CardDetails.Add(new CreateCardDto()
            {
                CardNumber = "",
                CVVNo = "",
                ExpiryDate = "",
                HolderName = "",
                Type = "",
            });

            ActionResult<string> response = _userController.CreateUser(user);
            Assert.IsType<NotFoundObjectResult>(response.Value);

            ActionResult<string> response1 = _userController.CreateUser(user);
            Assert.IsType<string>(response1.Value);

            ActionResult<string> response2 = _userController.CreateUser(user);
            Assert.IsType<string>(response2.Value);
        }
    }
}
