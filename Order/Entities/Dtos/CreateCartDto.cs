using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Entities.Dtos
{
    public class CreateCartDto 
    {
        ///<summary>
        /// wishlist name
        ///</summary>
        [Required]
        public int Quantity { get; set; }

        ///<summary>
        /// product list
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "product_id")]
        public Guid ProductId { get; set; }
    }
}
