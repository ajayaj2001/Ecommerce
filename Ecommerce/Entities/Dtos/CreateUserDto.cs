using Customer.Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class CreateUserDto
    {
        ///<summary>
        ///first name of user
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        ///<summary>
        ///last name of user 
        ///</summary>
        [Required]
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
        [Required]
        public CreateUserCredentialDto Credentials { get; set; }

        ///<summary>
        ///addrss details list of user
        ///</summary>
        public ICollection<CreateAddressDto> Addresses { get; set; } = new List<CreateAddressDto>();

        ///<summary>
        ///phone details list of user 
        ///</summary>
        [JsonProperty(PropertyName = "card_details")]
        public ICollection<CreateCardDto> CardDetails { get; set; } = new List<CreateCardDto>();
    }
}
