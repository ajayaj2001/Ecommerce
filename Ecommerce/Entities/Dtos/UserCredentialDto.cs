using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UserCredentialDto
    {
        ///<summary>
        ///email address of user
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "email_address")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "enter valid email address")]
        public string EmailAddress { get; set; }

    }
}
