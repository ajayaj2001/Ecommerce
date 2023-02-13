using System;


namespace Product.Entities.Dtos
{
    public class ResultProductDto
    {
        ///<summary>
        ///unique id of field
        ///</summary>
        public Guid Id { get; set; }

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
        ///product category type
        ///</summary>
        public string Type { get; set; }
    }
}
