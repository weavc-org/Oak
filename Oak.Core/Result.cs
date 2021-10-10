using Oak.Core.Errors;

namespace Oak.Core
{
    public class Result : Result<string> 
    { 
        public Result(
            bool success = false,
            string message = null, 
            IError error = null) 
            : base(success: success, message: message, error: error)
        { 
        }
    }

    public class Result<TPayload>
    {
        public Result(
            bool success = false,
            string message = null, 
            TPayload payload = default, 
            IError error = null) 
        { 
            this.Populate(success, message, payload, error); 
        }

        public Result(
            bool success = false,
            string message = null, 
            TPayload payload = default, 
            string error = null) 
        { 
            this.Populate(success, message, payload, new BadRequestError(error)); 
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public IError Error { get; set; }
        public TPayload Payload { get; set; }

        public Result<TPayload> FromResult<T>(Result<T> result) 
        {
            this.Populate(
                success: result.Success, 
                message: result.Message,
                error: result.Error);

            if (result.Payload is TPayload)
            {
                // I hate this too
                this.Payload = (TPayload)(object)result.Payload;
            }

            return this;
        }

        public virtual Result<TPayload> Populate(
            bool success = false,
            string message = null, 
            TPayload payload = default, 
            IError error = null)
        {
            this.Success = success;
            this.Message = message;
            this.Payload = payload;
            this.Error = error;

            return this;
        }
    }
}   