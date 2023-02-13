using System;

namespace Order.Entities.Dtos
{
    public class WishListDto
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

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
