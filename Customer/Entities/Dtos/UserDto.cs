using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UserDto
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        ///first name of user
        ///</summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        ///<summary>
        ///last name of user 
        ///</summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        ///<summary>
        ///email address of user
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "email_address")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "enter valid email address")]
        public string EmailAddress { get; set; }

        ///<summary>
        ///user credentials
        ///</summary>
        public UserCredentialDto Credentials { get; set; }

        ///<summary>
        ///addrss details list of user
        ///</summary>
        public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();

        ///<summary>
        ///phone dettails list of user 
        ///</summary>
        [JsonProperty(PropertyName = "card_details")]
        public ICollection<CardDto> CardDetails { get; set; } = new List<CardDto>();
    }
}
