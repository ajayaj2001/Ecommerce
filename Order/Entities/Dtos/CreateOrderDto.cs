using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Entities.Dtos
{
    public class CreateOrderDto 
    {

        ///<summary>
        /// product id
        ///</summary>
        [JsonProperty(PropertyName = "card_id")]
        [Required]
        public Guid CardId { get; set; }

         ///<summary>
         /// product id
         ///</summary>
         [JsonProperty(PropertyName = "address_id")]
         [Required]
         public Guid AddressId { get; set; }
        }
}
