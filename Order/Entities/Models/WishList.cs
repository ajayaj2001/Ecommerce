using System;

namespace Order.Entities.Models
{
    public class WishList : BaseModel
    {
        ///<summary>
        /// wishlist name
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// user id
        ///</summary>
        public Guid UserId { get; set; }

        ///<summary>
        /// product list
        ///</summary>
        public Guid ProductId { get; set; }
    }
}
