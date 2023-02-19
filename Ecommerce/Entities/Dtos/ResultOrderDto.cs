using Customer.Entities.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;


namespace Order.Entities.Dtos
{
    public class ResultOrderDto
    {
        ///<summary>
        ///address detail of user
        ///</summary>
        public AddressDto address { get; set; }

        ///<summary>
        ///card details of user 
        ///</summary>
        public CardDto CardDetail { get; set; }
    }
}
