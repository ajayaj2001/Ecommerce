using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entities.Dtos
{
    public class CreateCardDto
    {
        ///<summary>
        /// card holder name 
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "card_holder_name")]
        public string HolderName { get; set; }

        ///<summary>
        /// card number
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "card_number")]
        public string CardNumber { get; set; }

        ///<summary>
        /// card expiry date
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "expiry_date")]
        public string ExpiryDate { get; set; }

        ///<summary>
        /// card cvv number 
        ///</summary>
        [Required]
        [JsonProperty(PropertyName = "cvv_number")]
        public string CVVNo { get; set; }


        ///<summary>
        /// payment type 
        ///</summary>
        [Required]
        public string Type { get; set; }
    }
}
