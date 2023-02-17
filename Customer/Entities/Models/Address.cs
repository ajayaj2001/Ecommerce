using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Entities.Models
{
    public class Address : BaseModel
    {
        ///<summary>
        /// address type 
        ///</summary>
        public string Type { get; set; }

        ///<summary>
        ///  user id of who creaated this
        ///</summary>
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        ///<summary>
        /// address line1 of user
        ///</summary>
        public string Line1 { get; set; }

        ///<summary>
        /// address line2 of user
        ///</summary>
        public string Line2 { get; set; }

        ///<summary>
        /// address city of user
        ///</summary>
        public string City { get; set; }

        ///<summary>
        /// address state name of user
        ///</summary>
        public string StateName { get; set; }

        ///<summary>
        /// address country of user
        ///</summary>
        public string Country { get; set; }

        ///<summary>
        /// address zipcode of user
        ///</summary>
        public string Zipcode { get; set; }

        ///<summary>
        /// phone number
        ///</summary>
        public string PhoneNumber { get; set; }
    }
}
