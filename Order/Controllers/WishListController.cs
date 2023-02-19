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

namespace Order.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class WishListController : Controller
    {
        private readonly IWishListService _wishListService;
        private readonly ICartService _cartService;
        private readonly ILogger _logger;

        public WishListController(ILogger logger, IWishListService wishListService, ICartService cartService)
        {
            _wishListService = wishListService ?? throw new ArgumentNullException(nameof(wishListService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        ///<summary> 
        ///Create wishlist
        ///</summary>
        ///<remarks>To create wishlist</remarks> 
        ///<param name="wishList"></param> 
        ///<response code = "200" >Id of created wishlist returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "409" >wish list already exist</response>
        ///<response code = "404" >product not found</response>
        ///<response code="500">Internel server error</response>
        [HttpPost("wishlist")]
        [SwaggerOperation(Summary = "Create Wishlist", Description = "To create wishlist with product")]
        [SwaggerResponse(200, "Created", typeof(CreatedSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> CreateWishList([FromBody] CreateWishListDto wishList)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_cartService.GetProductById(wishList.ProductId) == null)
            {
                _logger.LogError("Product not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wishlist product not found", errorType = "create-wishlist" });
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
        ///Delete wishlist by wishlist name
        ///</summary>
        ///<remarks>To delete an wishlist by wishlist name</remarks> 
        ///<param name="wishlistName"></param> 
        ///<response code = "200" >wishlist deleted successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >wishlist not found</response>
        ///<response code="500">Internel server error</response>
        [HttpDelete("wishlist/{wishlistName}")]
        [SwaggerOperation(Summary = "Delete wishlist", Description = "To delete an wishlist from database")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult DeleteWishlist(string wishlistName)
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
        ///Delete wishlist for user
        ///</summary>
        ///<remarks>To delete wishlist for user</remarks> 
        ///<param name="id"></param> 
        ///<response code = "200" >wishlist delted successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >wishlist not found</response>
        ///<response code="500">Internel server error</response>
        [HttpDelete("wishlist")]
        [SwaggerOperation(Summary = "Delete wishlist", Description = "To delete an wishlist from database")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult DeleteWishlistProduct([FromBody] CreateWishListDto wishList)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!_wishListService.checkIfAlreadyExist(wishList, authId))
            {
                _logger.LogInformation("Product not exist in wishlist");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wish list with product not found", errorType = "delete-all-wishlist" });
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
        ///<response code = "200" >get wishlist detail based on wishslist name returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >wishlist not found</response>
        ///<response code="500">Internel server error</response>
        [HttpGet("wishlist/{wishlistName}")]
        [SwaggerOperation(Summary = "Get wishlist", Description = "To get an wishlist details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(WishList))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult GetWishlistByName(string wishlistName)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_wishListService.checkWishListExist(wishlistName, authId))
            {
                _logger.LogError("wishlist not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wishlist not found", errorType = "get-wishlist" });
            }

            _logger.LogInformation("Returned wishlist based on name ");
            return Ok(_wishListService.GetWishListByName(wishlistName, authId));
        }

        ///<summary> 
        ///move wishlist to cart 
        ///</summary>
        ///<remarks>To move wishlist to cart</remarks> 
        ///<param name="wishlistName"></param> 
        ///<response code = "200" >moved wishlist to cart successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >wishlist not found</response>
        ///<response code="500">Internel server error</response>
        [HttpGet("wishlisttocart/{wishlistName}")]
        [SwaggerOperation(Summary = "Get wishlist", Description = "To move wishlist products to cart")]
        [SwaggerResponse(200, "Success", typeof(WishList))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult MoveWishListToCart(string wishlistName)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_wishListService.checkWishListExist(wishlistName, authId))
            {
                _logger.LogError("wishlist not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "wishlist not found", errorType = "move-wishlist" });
            }
            _logger.LogInformation("successfully moved");
            _wishListService.MoveWishListToCart(wishlistName, authId);
            return Ok("successfully moved");
        }

        ///<summary> 
        ///Get Wishlist 
        ///</summary>
        ///<remarks>To get an wishlist details stored in the database</remarks> 
        ///<response code = "200" >get wishlist based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >wishlist not found</response>
        ///<response code="500">Internel server error</response>
        [HttpGet("wishlist")]
        [SwaggerOperation(Summary = "Get wishlist", Description = "To get an wishlist details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(WishList))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult GetWishlist()
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _logger.LogInformation("Returned wishlist based on name ");
            return Ok(_wishListService.GetWishListForUser(authId));
        }

    }
}
