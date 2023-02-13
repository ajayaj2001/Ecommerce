using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UpdateUserCredentialDto
    {
        ///<summary>
        ///user name of user
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        ///<summary>
        ///password user
        ///</summary>
        [Required]
        public string Password { get; set; }

        ///<summary>
        ///role of user
        ///</summary>
        [Required]
        public string Role { get; set; }
    }
}
