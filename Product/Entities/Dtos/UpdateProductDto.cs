using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities.Dtos
{
    public class UpdateProductDto
    {

        ///<summary>
        /// product name
        ///</summary>
        [Required]
        public string Name { get; set; }

        ///<summary>
        /// product description
        ///</summary>
        [Required]
        public string Description { get; set; }

        ///<summary>
        /// product quantity
        ///</summary>
        [Required]
        public int Quantity { get; set; }

        ///<summary>
        /// product visibility
        ///</summary>
        [Required]
        public bool Visibility { get; set; }

        ///<summary>
        ///product category type
        ///</summary>
        [Required]
        public string Type { get; set; }
    }
}
