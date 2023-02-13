using Product.Entities.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Order.Entities.Dtos
{
    public class FetchWishListDto
    {

        ///<summary>
        /// wishlist name
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// product list
        ///</summary>
        public ICollection<ResultProductDto> ProductList { get; set; } = new List<ResultProductDto>();
    }
}
