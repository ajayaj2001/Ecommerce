using System;
using System.Collections.Generic;
using System.Text;

namespace JWTAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        ///<summary>
        /// user name
        ///</summary>
        public string EmailAddress { get; set; }

        ///<summary>
        /// jwt token
        ///</summary>
        public string JwtToken { get; set; }

        ///<summary>
        /// token expires time
        ///</summary>
        public int ExpiresIn { get; set; }  
    }
}
