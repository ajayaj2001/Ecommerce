using System;

namespace Order.Entities.Dtos
{
    public class UpdateProductQuantityDto
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        /// product quantity
        ///</summary>
        public int Quantity { get; set; }

        ///<summary>
        /// user Id
        ///</summary>
        public Guid UserId { get; set; }
    }
}
