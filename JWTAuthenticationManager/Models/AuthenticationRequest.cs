using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JWTAuthenticationManager.Models
{
    public class AuthenticationRequest
    {
        ///<summary>
        /// user id
        ///</summary>
        [Required]
        public Guid Id { get; set; }

        ///<summary>
        ///user name of user
        ///</summary>
        [JsonProperty(PropertyName = "user_name")]
        [Required]
        public string UserName { get; set; }

        ///<summary>
        ///password user
        ///</summary>
        [Required]
        public string Password { get; set; }

        ///<summary>
        ///user role
        ///</summary>
        [Required]
        public string Role { get; set; }


    }
}
