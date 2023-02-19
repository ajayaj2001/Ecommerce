using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Customer.Entities.Models
{
    public class UserCredential : BaseModel
    {
        ///<summary>
        ///unique email address
        ///</summary>
        [Required]
        public string EmailAddress { get; set; }

        ///<summary>
        ///password of user
        ///</summary>
        [Required]
        public string Password { get; set; }

        ///<summary>
        ///role of user
        ///</summary>
        [Required]
        public string Role { get; set; }

        ///<summary>
        ///  user id of who creaated this
        ///</summary>
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
    }
}
