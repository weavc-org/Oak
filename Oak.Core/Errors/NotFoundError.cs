namespace Oak.Core.Errors
{
    public class NotFoundError : IError
    {
        private readonly string _message;

        public NotFoundError(string message = null)
        {
            this._message = message;
        }

        public string ErrorType => "NotFound";
        public int StatusCode => 404;
        public string StandardMessage => "Resource not found";
        public string ErrorMessage => this._message;
        public object Details => this._message;
    }
}
 