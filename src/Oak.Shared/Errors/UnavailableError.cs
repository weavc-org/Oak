namespace Oak.Shared.Errors
{
    public class UnavailableError : IError
    {
        private readonly string _message;
        private readonly string _customStandardMessage;

        public UnavailableError(string message = null, string customStandardMessage = null)
        {
            this._message = message;
            this._customStandardMessage = customStandardMessage;
        }

        public string ErrorType => "Unavailable";
        public int StatusCode => 503;
        public string StandardMessage => this._customStandardMessage ?? "Bad Request";
        public string ErrorMessage => this._message;
        public object Details => this._message;
    }
}
 