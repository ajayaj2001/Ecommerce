using Newtonsoft.Json;
using System;

namespace Customer.Entities.Dtos
{
    public class UserCredentialDto
    {
        ///<summary>
        ///user name of user
        ///</summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        ///<summary>
        ///password user
        ///</summary>
        //public string Password { get; set; }

        ///<summary>
        ///user role
        ///</summary>
        //public string Role { get; set; }

        ///<summary>
        ///user id of who created address
        ///</summary>
        //public Guid UserId { get; set; }
    }
}
