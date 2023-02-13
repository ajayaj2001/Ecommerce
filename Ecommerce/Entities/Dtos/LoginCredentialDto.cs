using Newtonsoft.Json;
using System;

namespace Customer.Entities.Dtos
{
    public class LoginCredentialDto
    {
        ///<summary>
        ///user name of user
        ///</summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        ///<summary>
        ///password user
        ///</summary>
        public string Password { get; set; }
    }
}
