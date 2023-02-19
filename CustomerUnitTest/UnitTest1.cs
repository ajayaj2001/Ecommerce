using AutoMapper;
using Customer.Contracts.Repositories;
using Customer.Contracts.Services;
using Customer.Controllers;
using Customer.DbContexts;
using Customer.Entities.Dtos;
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

            _jwtTokenHandler = new JWTTokenHandler();
            _userRepository = new UserRepository(_context);
            _authService = new AuthService(_userRepository);
            _userService = new UserService(_mapper, _userRepository, _logger);
            _userController = new UserController(_logger, _userService);
            _authController = new AuthController(_logger, _authService, _jwtTokenHandler);
            
            string userId = "5bfdfa9f-ffa2-4c31-40de-08db05cf468e";
            Mock<HttpContext> contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier,userId)
                                        // other required and custom claims
                           }, "TestAuthentication")));
            _userController.ControllerContext.HttpContext = contextMock.Object;
            _authController.ControllerContext.HttpContext = contextMock.Object;
        }

        Guid userId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");

        /// <summary>
        ///   To test get user details by user id
        /// </summary>
        [Fact]
        public void GetUserById_OkObjectResult()
        {
            ActionResult response = _userController.GetUserById(userId) as ActionResult;
            Assert.IsType<OkObjectResult>(response);
        }

        /// <summary>
        ///   To test get user by invalid user id
        /// </summary>
        [Fact]
        public void GetUserById_NotFoundObjectResult()
        {
            Guid userId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4684");
            ActionResult response = _userController.GetUserById(userId) as ActionResult;
            Assert.IsType<NotFoundObjectResult>(response);
        }

        /// <summary>
        ///   To test create new user with valid user details 
        /// </summary>
        [Fact]
        public void CreateUser_OkObjectResult()
        {
            CreateUserDto user = new CreateUserDto()
            {
                FirstName = "ajay",
                LastName = "kumar",
                
            };
            user.CardDetails.Add(new CreateCardDto()
            {
                CardNumber = "12312312",
                CVVNo = "123",
                ExpiryDate = "12/27",
                HolderName = "Ajay",
                Type = "Personal",
            });
            user.Addresses.Add(new CreateAddressDto()
            {
                Line1 = "anna nagar",
                Line2 = "velachery",
                City = "chennai",
                StateName = "tamil nadu",
                Type = "personal",
                Country = "tamil nadu",
                Zipcode = "626101",
                PhoneNumber = "1234567890"
            });
            user.Credentials=new CreateUserCredentialDto()
            {
                EmailAddress = "ajay@ajay.live",
                Password = "aasdASDF@#$234",
                Role = "customer",
            };

            ActionResult<string> response = _userController.CreateUser(user);
            Assert.IsType<OkObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test create new user with existing user details 
        /// </summary>
        [Fact]
        public void CreateUser_ConflitObjectResult()
        {
            CreateUserDto user = new CreateUserDto()
            {
                FirstName = "ajay",
                LastName = "kumar",
               
            };

            user.CardDetails.Add(new CreateCardDto()
            {
                CardNumber = "12312312",
                CVVNo = "123",
                ExpiryDate = "12/27",
                HolderName = "Ajay",
                Type = "work",
            });

            user.Addresses.Add(new CreateAddressDto()
            {
                Line1 = "anna nagar",
                Line2 = "velachery",
                City = "chennai",
                StateName = "tamil nadu",
                Type = "work",
                Country = "tamil nadu",
                Zipcode = "626101",
                PhoneNumber = "1234567890"
            });

            user.Credentials = new CreateUserCredentialDto()
            {
                EmailAddress = "tester@gmail.com",
                Password = "aasdASDF@#$234",
                Role = "customer",
            };

            ActionResult<string> response = _userController.CreateUser(user);
            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test update user with user details 
        /// </summary>
        [Fact]
        public void UpdateUser_OkObjectResult()
        {
            UpdateUserDto user = new UpdateUserDto()
            {
                FirstName = "ajay",
                LastName = "kumar",
                
            };

            user.CardDetails.Add(new UpdateCardDto()
            {
                CardNumber = "12312312",
                CVVNo = "123",
                ExpiryDate = "12/27",
                HolderName = "Ajay",
                Type = "work",
            });

            user.Addresses.Add(new UpdateAddressDto()
            {
                Line1 = "anna nagar",
                Line2 = "velachery",
                City = "chennai",
                StateName = "tamil nadu",
                Type = "work",
                Country = "tamil nadu",
                Zipcode = "626101",
                PhoneNumber = "1234567890"
            });

            user.Credentials = new UpdateUserCredentialDto()
            {
                EmailAddress = "ajay@ajay.live",
                Password = "aasdASDF@#$234",
            };

            ActionResult<string> response = _userController.UpdateUser(userId,user);
            Assert.IsType<OkObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test update user with invalid user details 
        /// </summary>
        [Fact]
        public void UpdateUser_NotFoundObjectResult()
        {
            UpdateUserDto user = new UpdateUserDto()
            {
                FirstName = "ajay",
                LastName = "kumar",
                
            };

            user.CardDetails.Add(new UpdateCardDto()
            {
                CardNumber = "12312312",
                CVVNo = "123",
                ExpiryDate = "12/27",
                HolderName = "Ajay",
                Type = "work",
            });

            user.Addresses.Add(new UpdateAddressDto()
            {
                Line1 = "anna nagar",
                Line2 = "velachery",
                City = "chennai",
                StateName = "tamil nadu",
                Type = "work",
                Country = "tamil nadu",
                Zipcode = "626101",
                PhoneNumber = "1234567890"
            });

            user.Credentials = new UpdateUserCredentialDto()
            {
                EmailAddress = "ajay@ajay.live",
                Password = "aasdASDF@#$234",
            };
            Guid userId2 = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf4685");
            ActionResult<string> response = _userController.UpdateUser(userId2, user);
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        /// <summary>
        ///  To test update user with existing user details 
        /// </summary>
        [Fact]
        public void UpdateUser_ConflitObjectResult()
        {
            UpdateUserDto user = new UpdateUserDto()
            {
                FirstName = "ajay",
                LastName = "kumar",
               
            };

            user.CardDetails.Add(new UpdateCardDto()
            {
                CardNumber = "12312312",
                CVVNo = "123",
                ExpiryDate = "12/27",
                HolderName = "Ajay",
                Type = "personal",
            });

            user.CardDetails.Add(new UpdateCardDto()
            {
                CardNumber = "12312312",
                CVVNo = "123",
                ExpiryDate = "12/27",
                HolderName = "Ajay",
                Type = "personal",
            });

            user.Addresses.Add(new UpdateAddressDto()
            {
                Line1 = "anna nagar",
                Line2 = "velachery",
                City = "chennai",
                StateName = "tamil nadu",
                Type = "personal",
                Country = "tamil nadu",
                Zipcode = "626101",
                PhoneNumber = "1234567890"
            });

            user.Credentials = new UpdateUserCredentialDto()
            {
                EmailAddress = "ajay@gmail.com",
                Password = "aasdASDF@#$234",
            };
            ActionResult<string> response = _userController.UpdateUser(userId, user);
            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        /// <summary>
        ///   To test login user by invalid detail
        /// </summary>
        [Fact]
        public void LoginUser_UnauthorizedObjectResult()
        {
            LoginCredentialDto loginCredential = new LoginCredentialDto() { EmailAddress = "ajay@gmail.com", Password= "tester2001" };
             ActionResult response = _authController.UserLogin(loginCredential) as ActionResult;
            Assert.IsType<UnauthorizedObjectResult>(response);
        }

        /// <summary>
        ///   To test login user by valid detail
        /// </summary>
        [Fact]
        public void LoginUser_OkObjectResult()
        {
            LoginCredentialDto loginCredential = new LoginCredentialDto() { EmailAddress = "tester@gmail.com", Password = "tester2001" };
            ActionResult response = _authController.UserLogin(loginCredential) as ActionResult;
            Assert.IsType<OkObjectResult>(response);
        }

    }
}
