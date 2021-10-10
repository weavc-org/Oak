namespace Oak.Core.Errors
{
    /// <summary>
    /// For one off errors
    /// </summary>
    public class CustomError : IError
    {
        public CustomError(
            int statusCode, 
            string errorType, 
            string standardMessage, 
            string errorMessage = null, 
            object details = null)
        {
            this.StatusCode = statusCode;
            this.ErrorType = errorType;
            this.StandardMessage = standardMessage;
            this.ErrorMessage = errorMessage;
            this.Details = details;
        }

        public string ErrorType { get; set; }
        public int StatusCode { get; set; }
        public string StandardMessage { get; set; }
        public string ErrorMessage { get; set; }
        public object Details { get; set; }
    }
}
 