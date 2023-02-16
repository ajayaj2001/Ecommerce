using System;

namespace Order.Entities.Models
{
    public class Cart : BaseModel
    {
        ///<summary>
        /// quantity
        ///</summary>
        public int Quantity { get; set; }

        ///<summary>
        /// user id
        ///</summary>
        public Guid UserId { get; set; }

        ///<summary>
        /// product id
        ///</summary>
        public Guid ProductId { get; set; }

        ///<summary>
        /// order id
        ///</summary>
        public Guid OrderId { get; set; } 

    }
}
