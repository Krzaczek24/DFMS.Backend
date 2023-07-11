using Core.WebApi.Exceptions;
using Core.WebApi.Extensions;
using Core.WebApi.Middlewares;
using DFMS.WebApi.Core.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace DFMS.WebApi.Core.Middlewares
{
    public static class MiddlewareHelper
    {
        public static IApplicationBuilder UseRestMiddleware(this IApplicationBuilder app) => app.UseMiddleware<Middleware>();
    }

    public class Middleware : LoggingMiddleware
    {
        public Middleware(RequestDelegate next) : base(next) { }

        protected override object GetErrorResponse(Exception exception)
        {
            if (exception is HttpErrorException<ErrorModel>)
            {
                var httpErrorException = (HttpErrorException<ErrorModel>)exception;
                return new ErrorResponse(httpErrorException.Errors);
            }

            return new ErrorResponse(new ErrorModel()
            {
                Code = ErrorCode.UNKNOWN,
                Message = exception.Message
            });
        }

        protected override void LogRequest(HttpContext httpContext, string bodyText)
        {
            Logger.Info($"REQUEST  {httpContext.GetRequestId()} | {httpContext.Request.GetPath()} | {bodyText}");
        }

        protected override void LogResponse(HttpContext httpContext, string bodyText)
        {            
            Logger.Info($"RESPONSE {httpContext.GetRequestId()} | {httpContext.Response.StatusCode} | {bodyText}");
        }
    }
}
