namespace Product.Entities.ResponseTypes
{
    public class ErrorResponse
    {
        ///<summary>
        /// detailed error message 
        ///</summary>
     
        public string errorMessage { get; set; } 

        public int errorCode { get; set; }

        public string errorType { get; set; }
    }
}
