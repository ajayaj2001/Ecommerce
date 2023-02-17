using Customer.Contracts.Services;
using Customer.Entities.Dtos;
using Customer.Entities.Models;
using Customer.Entities.ResponseTypes;
using JWTAuthenticationManager;
using JWTAuthenticationManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authServices;
        private readonly ILogger _logger;
        private readonly JWTTokenHandler _jwtTokenHandler;

        public AuthController(ILogger logger, IAuthService authServices, JWTTokenHandler jwtTokenHandler)
        {
            _authServices = authServices ?? throw new ArgumentNullException(nameof(authServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jwtTokenHandler = jwtTokenHandler ?? throw new ArgumentNullException(nameof(jwtTokenHandler));
        }

        ///<summary> 
        ///login user
        ///</summary>
        ///<remarks>To validate user detail and return session key</remarks> 
        ///<param name="loginCredentials"></param> 
        ///<response code = "200" >session token returned succesfully</response> 
        ///<response code = "401" >User credientials invalid</response> 
        ///<response code="500">Internel server error</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login User", Description = "validate user detail and return session token")]
        [SwaggerResponse(200, "Success", typeof(LoginSuccessResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public IActionResult UserLogin([FromBody] LoginCredentialDto loginCredentials)
        {
            //check username
            if (loginCredentials.UserName == null || loginCredentials.Password == null)
            {
                _logger.LogError("User name or password Empty");
                return Unauthorized(new ErrorResponse { errorMessage = "user name or password is Empty", errorCode = 401, errorType = "user-login" });
            }
            //is username exist
            UserCredential user = _authServices.GetUserByUserName(loginCredentials.UserName);
            if (user == null)
            {
                _logger.LogError("UserName not exist");
                return Unauthorized(new ErrorResponse { errorMessage = "userName not exist", errorCode = 401, errorType = "user-login" });
            };
            //is password same
            bool check = _authServices.ComparePassword(user.Password, loginCredentials.Password);
            if (!check)
            {
                _logger.LogError("Wrong password");
                return Unauthorized(new ErrorResponse { errorMessage = "wrong password", errorCode = 401, errorType = "user-login" });
            }
            AuthenticationResponse authenticationResponse = _jwtTokenHandler.GenerateJwtToken(new AuthenticationRequest() { Password = user.Password, Role = user.Role, UserName = user.UserName,Id=user.Id });
            _logger.LogInformation("Session created successfully");
            return Ok(new LoginSuccessResponse { access_token = authenticationResponse.JwtToken, token_type = "Bearer" });
        }
    }
}
