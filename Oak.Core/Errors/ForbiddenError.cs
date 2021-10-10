namespace Oak.Core.Errors
{
    public class ForbiddenError : IError
    {
        private readonly string _message;

        public ForbiddenError(string message = null)
        {
            this._message = message;
        }

        public string ErrorType => "Forbidden";
        public int StatusCode => 403;
        public string StandardMessage => "Cannot access resource";
        public string ErrorMessage => this._message;
        public object Details => this._message;
    }
}
 