namespace Customer.Entities.ResponseTypes
{
    public class LoginSuccessResponse
    {
        ///<summary>
        /// token type
        ///</summary>
        public string token_type { get; set; }

        ///<summary>
        /// session token 
        ///</summary>
        public string access_token { get; set; }
    }
}
