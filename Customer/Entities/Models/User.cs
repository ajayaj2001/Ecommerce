using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Models
{
    public class User : BaseModel
    {
        ///<summary>
        ///first name of user
        ///</summary>
        [Required]
        public string FirstName { get; set; }

        ///<summary>
        ///last name of user
        ///</summary>
        [Required]
        public string LastName { get; set; }

        
        ///<summary>
        ///unique email address
        ///</summary>
        public string EmailAddress { get; set; }

        ///<summary>
        ///user credentials
        ///</summary>
        public UserCredential Credentials { get; set; }

        ///<summary>
        ///address details of user
        ///</summary>
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        ///<summary>
        ///user profile image details
        ///</summary>
        public ICollection<CardDetail> CardDetails { get; set; } = new List<CardDetail>();

    }
}
