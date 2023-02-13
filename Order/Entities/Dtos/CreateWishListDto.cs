using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Entities.Dtos
{
    public class CreateWishListDto
    {
        ///<summary>
        /// wishlist name
        ///</summary>
        [Required]
        public string Name { get; set; }

        ///<summary>
        /// product list
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "product_id")]
        public Guid ProductId { get; set; }
    }
}
