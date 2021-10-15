namespace Oak.Errors
{
    /// <summary>
    /// For one off errors
    /// </summary>
    public class CustomError<T> : IError<T>
    {
        public CustomError(
            int statusCode, 
            string errorType, 
            string standardMessage, 
            string errorMessage = null, 
            T details = default)
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
        public T Details { get; set; }
    }
}
 