using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using Order.Entities.Models;
using Order.Entities.ResponseTypes;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Order.Controllers
{
    [ApiController]
    [Route("api")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ILogger _logger;

        public CartController(ILogger logger, ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary> 
        ///add product to cart 
        ///</summary>
        ///<remarks>To create cart and add product to it</remarks> 
        ///<param name="cartDetail"></param> 
        ///<response code = "200" >Id of created cart returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "409" >cart details is not valid</response>
        ///<response code = "404" >product not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpPost("cart")]
        [SwaggerOperation(Summary = "add to cart", Description = "To add product to cart with quantity")]
        [SwaggerResponse(200, "Created", typeof(CreatedSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> AddToCart([FromBody] CreateCartDto cartDetail)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

            ResultProductDto product = _cartService.GetProductById(cartDetail.ProductId, token);
            if (product == null)
            {
                _logger.LogError("product not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product not found", errorType = "add-to-cart" });
            }
            if (cartDetail.Quantity > product.Quantity)
            {
                _logger.LogError("quantity exceed the limit");
                return BadRequest(new ErrorResponse { errorCode = 400, errorMessage = "product quantity exceed", errorType = "add-to-cart" });
            }
            if (_cartService.checkProductExist(cartDetail.ProductId, authId))
            {
                _logger.LogError("product already exist");
                return Conflict(new ErrorResponse { errorCode = 409, errorMessage = "product already exist", errorType = "add-to-cart" });
            }
            _logger.LogInformation("product added to cart successfully");
            return Ok(_cartService.AddToCart(cartDetail, authId));
        }

        ///<summary> 
        ///Get cart 
        ///</summary>
        ///<remarks>To get an cart details stored in the database</remarks> 
        ///<response code = "200" >get cart under user returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("cart")]
        [SwaggerOperation(Summary = "Get cart", Description = "To get an cart details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(List<ReturnCartDto>))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetCart()
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            _logger.LogInformation("Returned cart under user");
            return Ok(_cartService.GetCartForUser(authId, token));
        }

        ///<summary> 
        ///Update cart product
        ///</summary>
        ///<remarks>To update the cart product quantity</remarks> 
        ///<param name=""></param> 
        ///<param name="id"></param>
        ///<response code = "200" >cart updated successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code="400">Invaild product detail</response>
        ///<response code = "409" >cart detail invalid</response>
        ///<response code = "404" >product not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpPut("cart")]
        [SwaggerOperation(Summary = "Update cart", Description = "To update the existing product details of cart")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> UpdateCart([FromBody] CreateCartDto cartDetail)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

            ResultProductDto product = _cartService.GetProductById(cartDetail.ProductId, token);
            if (product == null)
            {
                _logger.LogError("Product not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product not found", errorType = "get-cart-id" });
            }

            if (cartDetail.Quantity > product.Quantity)
            {
                _logger.LogError("product out of stock in cart");
                return BadRequest(new ErrorResponse { errorCode = 400, errorMessage = "product not found", errorType = "get-cart-id" });
            }

            if (!_cartService.checkProductExist(cartDetail.ProductId, authId))
            {
                _logger.LogError("product not found");
                return Conflict(new ErrorResponse { errorCode = 409, errorMessage = "product already exist", errorType = "get-cart-id" });
            }

            _cartService.UpdateCartProduct(cartDetail, authId);
            _logger.LogInformation("Product updated on cart");
            return Ok("updated");
        }


        ///<summary> 
        ///Delete product from cart
        ///</summary>
        ///<remarks>To delete an product from cart database</remarks> 
        ///<param name="productId"></param> 
        ///<response code = "200" >cart delted successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >product not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpDelete("cart/product/{productId}")]
        [SwaggerOperation(Summary = "Delete cartProduct", Description = "To delete an product from cart")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult DeleteCartProduct(Guid productId)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!_cartService.checkProductExist(productId, authId))
            {
                _logger.LogError("product not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product not found", errorType = "delete-cart" });
            }
            _cartService.DeleteCartProduct(productId, authId);
            _logger.LogInformation("product deleted from cart");
            return Ok("product deleted from cart");
        }


        ///<summary> 
        ///Move cart to order 
        ///</summary>
        ///<remarks>To move an cart to order stored in the database</remarks> 
        ///<response code = "200" >order id returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code="400">product out of stock</response>
        ///<response code = "404" >cart is empty</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("carttoorder")]
        [SwaggerOperation(Summary = "Cart To Order", Description = "To move an cart to order stored in the database")]
        [SwaggerResponse(200, "Success", typeof(CreatedSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult MoveToOrder()
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "");

            List<Cart> cartDetails = _cartService.GetCartDetails(authId).ToList();
            if (cartDetails.Count() <= 0)
            {
                _logger.LogError("Cart is empty");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "cart is empty", errorType = "move-cart" });
            }
            _logger.LogInformation("Returned wishlist based on name ");
            string result = _cartService.UpdateOrderIdToCart(cartDetails, authId, token);
            if (result == "failed")
            {
                _logger.LogError("Product out of stock");
                return BadRequest(new ErrorResponse { errorCode = 400, errorMessage = "product out of stock", errorType = "move-cart" });
            }
            return Ok(result);
        }
    }
}
