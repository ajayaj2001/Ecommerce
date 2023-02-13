using Product.Contracts.Services;
using Product.Entities.Dtos;
using Product.Entities.ResponseTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Collections.Generic;
using Product.Entities.Models;
using System.Linq;
using AutoMapper;

namespace Product.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : Controller
    {
        private readonly IProductService _productServices;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper,ILogger logger, IProductService productServices)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
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
        [Authorize(Roles = "admin")]
        [HttpPost("product")]
        [SwaggerOperation(Summary = "Create Address Book", Description = "To create address book with first name, last name and their communication details")]
        [SwaggerResponse(200, "Created", typeof(CreatedSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> CreateProduct([FromBody] CreateProductDto product)
        {
            string claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid authId = Guid.Parse(claimId);

            Category category = _productServices.GetCategoryByName(product.Type);
            if (category == null)
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product type not found", errorType = "create-product" });
            else
            {
                product.Type = category.Id.ToString();
                _logger.LogInformation("product created successfully");
                return Ok(_productServices.CreateProduct(product, authId));
            }
        }

        ///<summary> 
        ///Get All Address Book 
        ///</summary>
        ///<remarks>To get all the address book stored in the database</remarks> 
        ///<param name="size"></param> 
        /// ///<param name="pageNo"></param> 
        /// ///<param name="sortBy"></param> 
        /// ///<param name="sortOrder"></param> 
        ///<response code = "200" >get all address book based on query returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("product", Name = "GetAllProduct")]
        [SwaggerOperation(Summary = "Get All Product", Description = "To get all the product stored in the database")]
        [SwaggerResponse(200, "Success", typeof(List<ProductDto>))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetAllProduct(int size = 10, [FromQuery(Name = "page-no")] int pageNo = 1, [FromQuery(Name = "sort-by")] string sortBy = "Name", [FromQuery(Name = "sort-order")] string sortOrder = "ASC", [FromQuery(Name = "category")] string category = "")
        {
            string role = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()[0];

            if (!(sortBy == "Name" || sortBy == "Description"))
            {
                _logger.LogError("SortBy value Not Found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "SortBy value Not Found", errorType = "get-users" });
            }

            if (category != "")
            {
                Category categoryFromRepo = _productServices.GetCategoryByName(category);
                if (categoryFromRepo == null)
                {
                    category = null;
                    return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product type not found", errorType = "create-product" });
                }
                category = categoryFromRepo.Id.ToString();
            }

            PageSortParam pageSortParam = new PageSortParam() { Size = size, PageNo = pageNo, SortBy = sortBy, SortOrder = sortOrder, Category = category };
            List<ResultProductDto> products = _productServices.GetAllProducts(pageSortParam,role);
            if (products == null)
            {
                _logger.LogError("No Product Found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "No AddressBook Found", errorType = "get-users" });
            }
            _logger.LogInformation("Returned all product book");
            return Ok(products);
        }

        ///<summary> 
        ///Get product 
        ///</summary>
        ///<remarks>To get an product details stored in the database</remarks> 
        ///<param name="id"></param> 
        ///<response code = "200" >get address book based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("product/{id}", Name = "GetProduct")]
        [SwaggerOperation(Summary = "Get Product", Description = "To get an product details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(ProductDetail))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetProductById(Guid id)
        {
            string role = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()[0];

            ProductDto foundProduct = _productServices.GetDetailedProductById(id);
            if (foundProduct == null || !(foundProduct.Visibility || role == "admin"))
            {
                _logger.LogError("product not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product not found", errorType = "get-product" });
            }
            _logger.LogInformation("Returned individual product ");
            return Ok(_mapper.Map<ResultProductDto>(foundProduct));
        }

        ///<summary> 
        ///Update Address Book 
        ///</summary>
        ///<remarks>To update the existing address book details like first name etc</remarks> 
        ///<param name="user"></param> 
        ///<param name="id"></param>
        ///<response code = "200" >Address book updated successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "409" >The user input is not valid</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize(Roles ="admin")]
        [HttpPut("product/{id}")]
        [SwaggerOperation(Summary = "Update Product", Description = "To update the existing product details like first name etc")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> UpdateProduct(Guid id, [FromBody] UpdateProductDto productInput)
        {
            Guid authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ProductDetail productFromRepo = _productServices.GetProductById(id);
            if (productFromRepo == null)
            {
                _logger.LogError("Product not found");
                return NotFound();
            }

            Category category = _productServices.GetCategoryByName(productInput.Type);
            if (category == null)
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "product type not found", errorType = "update-product" });

            _productServices.UpdateProduct(productInput, productFromRepo, authId,category.Id);
            _logger.LogInformation("Product updated");
            return Ok("updated");
        }

        ///<summary> 
        ///Delete product  
        ///</summary>
        ///<remarks>To delete an product from database</remarks> 
        ///<param name="id"></param> 
        ///<response code = "200" >product delted successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize(Roles = "admin")]
        [HttpDelete("product/{id}")]
        [SwaggerOperation(Summary = "Delete product Book", Description = "To delet an product from database")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult DeleteProduct(Guid id)
        {
            ProductDetail productFromRepo = _productServices.GetProductById(id);
            if (productFromRepo == null)
            {
                _logger.LogError("Product not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "Product not found", errorType = "delete-product" });
            }
            _productServices.DeleteProduct(id);
            _logger.LogInformation("Product deleted");
            return Ok("Product deleted");
        }

        ///<summary> 
        ///Get product by ids
        ///</summary>
        ///<remarks>To get an product details stored in the database</remarks> 
        ///<param name="id"></param> 
        ///<response code = "200" >get address book based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >AddressBook not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("product/getproducts", Name = "GetProducts")]
        [SwaggerOperation(Summary = "Get Products" , Description = "To get an product details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(ProductDetail))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetProductByIdsService(List<Guid> ids)
        {
            _logger.LogInformation("Returned  product details ");
            return Ok(_productServices.GetProductByIds(ids));
        }
    }
}