using Impar.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Impar.Common.DTO
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string? Message { get; }
        public HttpStatusCode StatusCode { get; }

        protected Result(T value, bool isSuccess, string? message, HttpStatusCode httpStatusCode)
        {
            Value = value;
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = httpStatusCode;
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, null, HttpStatusCode.OK);
        }

        public static Result<T> Failure<T>(T value, string message, HttpStatusCode? httpStatusCode = null)
        {
            return new Result<T>(value, false, message, httpStatusCode ?? HttpStatusCode.InternalServerError);
        }

        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>((T)result.Value, result.IsSuccess, result.Message, result.StatusCode);
        }

        public static implicit operator ActionResult(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }
            else
            {
                return new ObjectResult(new { result.Message })
                {
                    StatusCode = (int)result.StatusCode
                };
            }
        }
    }

    public class Result : Result<object>
    {
        public Result(bool isSuccess, string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
            : base(null, isSuccess, message, httpStatusCode)
        {
        }

        public static Result Success()
        {
            return new Result(true, Messages.GenericSuccess, HttpStatusCode.OK);
        }

        public static Result Failure(string message, HttpStatusCode? httpStatusCode = null)
        {
            return new Result(false, message, httpStatusCode ?? HttpStatusCode.InternalServerError);
        }
    }
}
