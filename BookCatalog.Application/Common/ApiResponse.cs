using System;
using System.Collections.Generic;

namespace BookCatalog.Application.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public T? Data { get; }
        public IReadOnlyList<string>? Errors { get; }
        public string? Message { get; }

        private ApiResponse(
            bool isSuccess,
            int statusCode,
            T? data = default,
            IReadOnlyList<string>? errors = null,
            string? message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
            Message = message;
        }

        public static ApiResponse<T> Success(
            T? data = default,
            int statusCode = 200,
            string? message = null)
        {
            return new ApiResponse<T>(true, statusCode, data, null, message);
        }

        public static ApiResponse<T> Fail(
            string error,
            int statusCode = 400,
            string? message = null)
        {
            return new ApiResponse<T>(false, statusCode, default, new List<string> { error }, message);
        }

        public static ApiResponse<T> Fail(
            IReadOnlyList<string> errors,
            int statusCode = 400,
            string? message = null)
        {
            return new ApiResponse<T>(false, statusCode, default, errors, message);
        }
    }
}