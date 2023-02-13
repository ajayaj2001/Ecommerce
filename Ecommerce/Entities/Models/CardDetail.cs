using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Customer.Entities.Models
{
    public class CardDetail : BaseModel
    {
        ///<summary>
        /// card holder name 
        ///</summary>
        public string HolderName { get; set; }

        ///<summary>
        /// card holder name 
        ///</summary>
        public string CardNumber { get; set; }

        ///<summary>
        /// card holder name 
        ///</summary>
        public string ExpiryDate { get; set; }

        ///<summary>
        /// card holder name 
        ///</summary>
        public string CVVNo { get; set; }

        ///<summary>
        /// user id of who uploaded
        ///</summary>
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        ///<summary>
        /// payment type 
        ///</summary>
        public string Type { get; set; }
    }
}
