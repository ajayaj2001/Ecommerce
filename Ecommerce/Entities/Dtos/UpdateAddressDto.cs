using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UpdateAddressDto
    {
        ///<summary>
        ///address type
        ///</summary>
        [Required]
        public string Type { get; set; }

        ///<summary>
        ///address line 1 of user
        ///</summary>
        [Required]
        public string Line1 { get; set; }

        ///<summary>
        ///address line 2 of user
        ///</summary>
        [Required]
        public string Line2 { get; set; }

        ///<summary>
        ///address city of user
        ///</summary>
        [Required]
        public string City { get; set; }

        ///<summary>
        ///address state name of user
        ///</summary>
        [Required]
        public string StateName { get; set; }

        ///<summary>
        ///address country of user
        ///</summary>
        [Required]
        public string Country { get; set; }

        ///<summary>
        ///address zipcode of user
        ///</summary>
        [Required]
        public string Zipcode { get; set; }

        ///<summary>
        /// phone number
        ///</summary>
        [Required]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Enter valid phone number")]
        public string PhoneNumber { get; set; }
    }
}
