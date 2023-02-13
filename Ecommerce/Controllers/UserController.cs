﻿using Customer.Contracts.Services;
using Customer.Entities.Dtos;
using Customer.Entities.Models;
using Customer.Entities.ResponseTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IUserService _userServices;
        private readonly ILogger _logger;

        public UserController(ILogger logger, IUserService userServices)
        {
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
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
        [AllowAnonymous]
        [HttpPost("customer")]
        [SwaggerOperation(Summary = "Create Address Book", Description = "To create address book with first name, last name and their communication details")]
        [SwaggerResponse(200, "Created", typeof(CreatedSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> CreateUser([FromBody] CreateUserDto user)
        {
            ValidateInputResponse validate = _userServices.ValidateUserInputCreate(user);
            if (validate.errorCode == 404)
            {
                return NotFound(new ErrorResponse { errorCode = validate.errorCode, errorMessage = validate.errorMessage, errorType = "create-addressbook" });
            }
            else if (validate.errorCode == 409)
            {
                return Conflict(new ErrorResponse { errorCode = validate.errorCode, errorMessage = validate.errorMessage, errorType = "create-addressbook" });
            }
            else
            {
                _logger.LogInformation("User created successfully");
                return Ok(_userServices.CreateUser(user));
            }
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
        [Authorize]
        [HttpPut("customer/{id}")]
        [SwaggerOperation(Summary = "Update Address Book", Description = "To update the existing address book details like first name etc")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public ActionResult<string> UpdateUser(Guid id, [FromBody] UpdateUserDto user)
        {
            User userFromRepo = _userServices.GetUserById(id);
            if (userFromRepo == null)
            {
                _logger.LogError("User not found");
                return NotFound();
            }
            //var users = _userServices.FetchSingleCustomerDetail(userFromRepo);

            ValidateInputResponse validate = _userServices.ValidateUserInputUpdate(user, id);
            if (validate.errorCode == 404)
            {
                return NotFound(new ErrorResponse { errorCode = validate.errorCode, errorMessage = validate.errorMessage, errorType = "update-addressbook" });
            }
            else if (validate.errorCode == 409)
            {
                return Conflict(new ErrorResponse { errorCode = validate.errorCode, errorMessage = validate.errorMessage, errorType = "update-addressbook" });
            }
            else
            {
                //User updatedUser = _userServices.FetchUserDetailsForUpdate(user);
                _userServices.UpdateUser(id, user, userFromRepo);
                _logger.LogInformation("Address type updated");
                return Ok("updated");
            }
        }

        ///<summary> 
        ///Get User Details 
        ///</summary>
        ///<remarks>To get an user details stored in the database</remarks> 
        ///<param name="id"></param> 
        ///<response code = "200" >get user details based on userId returned successfully</response> 
        ///<response code = "401" >Not an authorized user</response>
        ///<response code = "404" >user not found</response>
        ///<response code="500">Internel server error</response>
        [Authorize]
        [HttpGet("customer/{id}", Name = "GetUser")]
        [SwaggerOperation(Summary = "Get user Details", Description = "To get an user details stored in the database")]
        [SwaggerResponse(200, "Success", typeof(UserDto))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult GetUserById(Guid id)
        {
            User foundUser = _userServices.GetUserById(id);
            if (foundUser == null)
            {
                _logger.LogError("User not found");
                return NotFound(new ErrorResponse { errorCode = 404, errorMessage = "user not found", errorType = "get-addressbook" });
            }
            _logger.LogInformation("Returned individual address book ");
            return Ok(_userServices.FetchSingleCustomerDetail(foundUser));
        }
    }
}