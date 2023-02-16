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

    }
}
