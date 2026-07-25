using System;
using System.Collections.Generic;
using System.Linq;

namespace StockPilot.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public string? ErrorMessage { get; }
        public Dictionary<string, List<string>> ValidationError { get; }

       
        protected Result(int statusCode, bool isSuccess, string? errorMessage = null, Dictionary<string, List<string>> validationError = null!)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            ValidationError = validationError;
        }

  
        public static Result Success(int statusCode = 200) => new(statusCode, true);
        public static Result Failure(string errorMessage) => new(400, false, errorMessage);
        public static Result ValidationFailure(Dictionary<string, List<string>> error) =>
            new(400, false, string.Join("; ", error.SelectMany(s => s.Value)), error);
        public static Result NotFound(string errorMessage) => new(404, false, errorMessage);
        public static Result InternalServerError(string errorMessage) => new(500, false, errorMessage);
        public static Result Unauthorized(string errorMessage) => new(401, false, errorMessage);
        public static Result Forbidden(string errorMessage) => new(403, false, errorMessage);
        public static Result Conflict(string errorMessage) => new(409, false, errorMessage);
        public static Result UnprocessableEntity(string errorMessage) => new(422, false, errorMessage);
        public static Result NoContent() => new(204, true);
        public static Result Created() => new(201, true);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

      
        public Result(int statusCode, bool isSuccess, string? errorMessage = null, T value = default!, Dictionary<string, List<string>> validationError = null!)
            : base(statusCode, isSuccess, errorMessage, validationError)
        {
            Value = value;
        }

       
        public static Result<T> Success(T value, int statusCode = 200)
            => new(statusCode, true, null, value);

        
        public static new Result<T> Failure(string errorMessage)
            => new(400, false, errorMessage);

        public static new Result<T> ValidationFailure(Dictionary<string, List<string>> error)
            => new(400, false, string.Join("; ", error.SelectMany(s => s.Value)), default!, error);

        public static new Result<T> NotFound(string errorMessage)
            => new(404, false, errorMessage);

        public static new Result<T> InternalServerError(string errorMessage)
            => new(500, false, errorMessage);

        public static new Result<T> Unauthorized(string errorMessage)
            => new(401, false, errorMessage);

        public static new Result<T> Forbidden(string errorMessage)
            => new(403, false, errorMessage);

        public static new Result<T> Conflict(string errorMessage)
            => new(409, false, errorMessage);

        public static new Result<T> UnprocessableEntity(string errorMessage)
            => new(422, false, errorMessage);
    }
}
