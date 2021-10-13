namespace Oak.Shared.Errors
{
    public interface IError
    { 
        string ErrorType { get; }
        int StatusCode { get; }
        string StandardMessage { get; }
        string ErrorMessage { get; }
    }

    public interface IError<T> : IError
    {
        T Details { get; }
    }
}
 