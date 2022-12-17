using DFMS.WebApi.Constants.Enums;
using System;

namespace DFMS.WebApi.DataContracts
{
    public class ApiResponse
    {
        public ResponseStatus Status { get; private set; }
        public string? Message { get; private set; }

        public ApiResponse(ResponseStatus status = ResponseStatus.Success)
        {
            Status = status;
        }

        public ApiResponse(string message, ResponseStatus status = ResponseStatus.Success) : this(status)
        {
            Message = message;
        }

        public ApiResponse(Exception exception) : this(exception.Message, ResponseStatus.Failure) { }

        public ApiResponse SetStatus(ResponseStatus status)
        {
            Status = status;
            return this;
        }

        public ApiResponse SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public ApiResponse<T> SetResult<T>(T data) => (this as ApiResponse<T>)?.SetResult(data) ?? new ApiResponse<T>(data, Message, Status);

        public ApiResponse<T> As<T>() => new ApiResponse<T>(default, Message, Status);

        public static ApiResponse Unknown => new ApiResponse(ResponseStatus.Unknown);
        public static ApiResponse Success => new ApiResponse(ResponseStatus.Success);
        public static ApiResponse Warning => new ApiResponse(ResponseStatus.Warning);
        public static ApiResponse Failure => new ApiResponse(ResponseStatus.Failure);
    }

    public class ApiResponse<TResult> : ApiResponse
    {
        public TResult? Result { get; private set; }

        public ApiResponse(TResult? data = default, string? message = null, ResponseStatus status = ResponseStatus.Success) : base(message, status)
        {
            Result = data;
        }

        public ApiResponse(TResult data, Exception exception) : this(data, exception.Message, ResponseStatus.Failure) { }

        public ApiResponse<TResult> SetResult(TResult data)
        {
            Result = data;
            return this;
        }
    }
}
