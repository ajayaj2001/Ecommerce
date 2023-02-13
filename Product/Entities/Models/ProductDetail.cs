using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Entities.Models
{
    public class ProductDetail : BaseModel
    {
        ///<summary>
        /// product name
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// product description
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// product quantity
        ///</summary>
        public int Quantity { get; set; }

        ///<summary>
        /// product visibility
        ///</summary>
        public bool Visibility { get; set; }

        ///<summary>
        /// category Id
        ///</summary>
        [ForeignKey("categoryId")]
        public Guid CategoryId { get; set; }
    }
}
