using Order.Entities.Dtos;

namespace Order.Entities.Dtos
{
    public class ReturnCartDto
    {
        ///<summary>
        /// wishlist name
        ///</summary>
        public int Quantity { get; set; }

        ///<summary>
        /// product list
        ///</summary>
        public ResultProductDto Product { get; set; } = new ResultProductDto();
    }
}
