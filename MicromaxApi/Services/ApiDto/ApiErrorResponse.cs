using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api.motorstar.Services.ApiDto
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
        }
        public ApiErrorResponse(string errorMessage) { }
        public ApiErrorResponse(Exception exception) { }
        public ApiErrorResponse(ModelStateDictionary modelState)
        {
        }
        public List<ApiError> Errors { get; set; }

        public class ApiError
        {
            public ApiError(string errorCode) { }
            public ApiError(string errorCode, string errorMessage) { }
            public ApiError(string errorCode, Exception exception) { }
            public ApiError(string errorCode, string type, string errorMessage) { }

            public string Code { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
        }
    }
}
