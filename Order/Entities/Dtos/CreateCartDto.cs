using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Entities.Dtos
{
    public class CreateCartDto 
    {
        ///<summary>
        /// quantity
        ///</summary>
        [Required]
        public int Quantity { get; set; }

        ///<summary>
        /// product id
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "product_id")]
        public Guid ProductId { get; set; }
    }
}
