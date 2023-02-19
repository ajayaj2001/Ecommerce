using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UpdateUserCredentialDto
    {
        ///<summary>
        /// unique id field 
        ///</summary>
        [Key]
        public Guid Id { get; set; }

        ///<summary>
        ///email address of user
        ///</summary>
        [JsonProperty(PropertyName = "email_address")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "enter valid email address")]
        public string EmailAddress { get; set; }

        ///<summary>
        ///password user
        ///</summary>
        public string Password { get; set; }

    }
}
