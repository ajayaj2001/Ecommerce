namespace Order.Entities.ResponseTypes
{
    public class ErrorResponse
    {
        ///<summary>
        /// detailed error message 
        ///</summary>
        public string errorMessage { get; set; }

        ///<summary>
        /// error code
        ///</summary>
        public int errorCode { get; set; }

        ///<summary>
        /// error type 
        ///</summary>
        public string errorType { get; set; }
    }
}
