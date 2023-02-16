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
        [JsonProperty(PropertyName = "product_id")]
        public Guid ProductId { get; set; }
    }
}
