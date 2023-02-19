using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class UpdateCardDto
    {
        ///<summary>
        /// unique id field 
        ///</summary>
        [Key]
        public Guid Id { get; set; }

        ///<summary>
        /// card holder name 
        ///</summary>
        [JsonProperty(PropertyName = "card_holder_name")]
        public string HolderName { get; set; }

        ///<summary>
        /// card number
        ///</summary>
        [JsonProperty(PropertyName = "card_number")]
        public string CardNumber { get; set; }

        ///<summary>
        /// card expiry date 
        ///</summary>
        [JsonProperty(PropertyName = "expiry_date")]
        public string ExpiryDate { get; set; }

        ///<summary>
        /// card cvv number
        ///</summary>
        [JsonProperty(PropertyName = "cvv_number")]
        public string CVVNo { get; set; }


        ///<summary>
        /// payment type 
        ///</summary>
        public string Type { get; set; }
    }
}
