namespace Oak.Core.Errors
{
    public interface IError
    {
        string ErrorType { get; }
        int StatusCode { get; }
        string StandardMessage { get; }
        string ErrorMessage { get; }
        object Details { get; }
    }
}
 