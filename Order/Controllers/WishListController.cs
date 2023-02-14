using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using Order.Entities.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System;
using Order.Entities.ResponseTypes;
using Order.Services;

namespace Order.Controllers
{
    [ApiController]
    [Route("api")]
    public class WishListController : Controller
    {
        private readonly IApiService _apiService;
        private readonly IWishListService _wishListService;
        private readonly ILogger _logger;

        public WishListController(ILogger logger, IWishListService wishListService, IApiService apiService)//, IProductService productService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _wishListService = wishListService ?? throw new ArgumentNullException(nameof(wishListService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary> 
        ///Create Address Book 
        ///</summary>
        ///<remarks>To create address book with first name, last name and their communication details</remarks> 
        ///<param name="user"></param> 
        ///<response code = "200" >Id of created address book returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "409" >The user input is not valid</response>
        ///<response code = "404" >MetaData type not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpPost("wishlist")]
        [SwaggerOperation(Summary = "Create Address Book", Description = "To create address book with first name, last name and their communication details")]
        [SwaggerResponse(200, "Created", typeof(CreatedSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> CreateWishList([FromBody] CreateWishListDto wishList)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

            ResultProductDto product = _apiService.GetProductById(wishList.ProductId, token);
            if (product == null)
            {
                _logger.LogError("Product not found");
                return NotFound();
            }

            if (_wishListService.checkIfAlreadyExist(wishList, authId))
            {
                _logger.LogInformation("wishlist with product already exist");
                return Conflict(new ErrorResponse { errorCode = 409, errorMessage = "wish list with product already exist", errorType = "create-wishlist" });
            }
            _logger.LogInformation("wishlist with product added successfully");
            return Ok(_wishListService.CreateWishList(wishList, authId));
        }

        ///<summary> 
        ///Delete wishlist by userName
        ///</summary>
        ///<remarks>To delete an address book frpm database</remarks> 
        ///<param name="wishlistName"></param> 
        ///<response code = "200" >address book delted successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpDelete("wishlist/{wishlistName}")]
        [SwaggerOperation(Summary = "Delete wishlist", Description = "To delete an wishlist from database")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult DeleteWishlist(string wishlistName)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_wishListService.checkWishListExist(wishlistName, authId))
            {
                _logger.LogError("wishlist not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wishlist not found", errorType = "delete-wishlist" });
            }
            _wishListService.DeleteWishlistByName(wishlistName, authId);
            _logger.LogInformation("wish list deleted");
            return Ok("wishlist deleted");
        }

        ///<summary> 
        ///Delete wishlist by userName
        ///</summary>
        ///<remarks>To delete an address book frpm database</remarks> 
        ///<param name="id"></param> 
        ///<response code = "200" >address book delted successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpDelete("wishlist")]
        [SwaggerOperation(Summary = "Delete wishlist product", Description = "To delete an wishlist product from database")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult DeleteWishlistProduct([FromBody] CreateWishListDto wishList)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_wishListService.checkIfAlreadyExist(wishList, authId))
            {
                _logger.LogInformation("Product not exist in wishlist");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wish list with product not found", errorType = "create-wishlist" });
            }
            _wishListService.DeleteWishlistProduct(wishList, authId);
            _logger.LogInformation("wish list product deleted");
            return Ok("wishlist product deleted");
        }

        ///<summary> 
        ///Get Wishlist 
        ///</summary>
        ///<remarks>To get an wishlist details stored in the database</remarks> 
        ///<param name="wishlistName"></param> 
        ///<response code = "200" >get address book based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("wishlist/{wishlistName}")]
        [SwaggerOperation(Summary = "Get wishlist", Description = "To get an wishlist details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(WishList))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetWishlistByName(string wishlistName)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

            if (!_wishListService.checkWishListExist(wishlistName, authId))
            {
                _logger.LogError("wishlist not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wishlist not found", errorType = "delete-wishlist" });
            }

            _logger.LogInformation("Returned wishlist based on name ");
            return Ok(_wishListService.GetWishListByName(wishlistName, authId,token));
        }

        ///<summary> 
        ///move wishlist to cart 
        ///</summary>
        ///<remarks>To move wishlist to cart</remarks> 
        ///<param name="wishlistName"></param> 
        ///<response code = "200" >get address book based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("wishlisttocart/{wishlistName}")]
        [SwaggerOperation(Summary = "Get wishlist", Description = "To get an wishlist details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(WishList))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult MoveWishListToCart(string wishlistName)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_wishListService.checkWishListExist(wishlistName, authId))
            {
                _logger.LogError("wishlist not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wishlist not found", errorType = "delete-wishlist" });
            }

            _logger.LogInformation("successfully moved");
            _wishListService.MoveWishListToCart(wishlistName, authId);
            return Ok("successfully moved");
        }

        ///<summary> 
        ///Get Wishlist 
        ///</summary>
        ///<remarks>To get an wishlist details stored in the database</remarks> 
        ///<param name="wishlistName"></param> 
        ///<response code = "200" >get address book based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("wishlist")]
        [SwaggerOperation(Summary = "Get wishlist", Description = "To get an wishlist details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(WishList))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetWishlist()
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            _logger.LogInformation("Returned wishlist based on name ");
            return Ok(_wishListService.GetWishListForUser(authId,token));
        }

    }
}
