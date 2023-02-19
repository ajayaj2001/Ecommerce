using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Entities.Dtos
{
    public class GetOrderDetailsDto 
    {

        ///<summary>
        /// product id
        ///</summary>
        [Required]
        public Guid CardId { get; set; }

         ///<summary>
         /// product id
         ///</summary>
         [Required]
         public Guid AddressId { get; set; }
        }
}
