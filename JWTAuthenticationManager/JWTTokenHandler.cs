using JWTAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthenticationManager
{
    public class JWTTokenHandler
    {
        public const string JWT_SECURITY_KEY = "yPkCqn4kSWL taJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
        private const int JWT_TOKEN_VALIDITY_MINS = 360;

        ///<summary>
        /// generate jwt token
        ///</summary>
        ///<param name="authenticationRequest"></param>
        public AuthenticationResponse GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {

            DateTime tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            byte[] tokenkey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim (JwtRegisteredClaimNames.Sub, authenticationRequest.Id.ToString()),
                new Claim (ClaimTypes.Role,authenticationRequest.Role)
            });

            SigningCredentials signingCredentials = new SigningCredentials(
                  new SymmetricSecurityKey(tokenkey),
                  SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                EmailAddress = authenticationRequest.EmailAddress,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }
    }
}
