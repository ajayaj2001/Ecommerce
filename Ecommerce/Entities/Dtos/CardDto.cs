using Newtonsoft.Json;
using System;

namespace Customer.Entities.Dtos
{
    public class CardDto
    {

        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

        ///<summary>
        /// card holder name 
        ///</summary>
        [JsonProperty(PropertyName = "card_holder_name")]
        public string HolderName { get; set; }

        ///<summary>
        /// card numer 
        ///</summary>
        [JsonProperty(PropertyName = "card_number")]
        public string CardNumber { get; set; }

        ///<summary>
        /// card expiry date
        /// ///</summary>
        [JsonProperty(PropertyName = "expiry_date")]
        public string ExpiryDate { get; set; }

        ///<summary>
        /// card cvv number 
        ///</summary>
        [JsonProperty(PropertyName = "cvv_number")]
        public string CVVNo { get; set; }

        ///<summary>
        /// card type 
        ///</summary>
        public string Type { get; set; }
    }
}
