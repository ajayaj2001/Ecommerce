using Newtonsoft.Json;
using System;

namespace Order.Entities.Dtos
{
    public class CartDto 
    {
        ///<summary>
        /// cart id
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        /// wishlist name
        ///</summary>
        public int Quantity { get; set; }

        ///<summary>
        /// user id
        ///</summary>
        public Guid UserId { get; set; }

        ///<summary>
        /// product list
        ///</summary>
        [JsonProperty(PropertyName = "product_id")]
        public Guid ProductId { get; set; }
    }
}
