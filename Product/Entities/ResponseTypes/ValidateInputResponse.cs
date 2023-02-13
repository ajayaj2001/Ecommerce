namespace Product.Entities.ResponseTypes
{
    public class ValidateInputResponse
    {
        ///<summary>
        /// error message 
        ///</summary>
        public string errorMessage { get; set; }

        ///<summary>
        /// error code to determain which type error
        ///</summary>
        public int errorCode { get; set; }
    }
}
