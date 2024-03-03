using Core.WebApi.Exceptions;
using Core.WebApi.Extensions;
using Core.WebApi.Middlewares;
using DFMS.WebApi.Common.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace DFMS.WebApi.Common.Middlewares
{
    public static class MiddlewareHelper
    {
        public static IApplicationBuilder UseRestMiddleware(this IApplicationBuilder app) => app.UseMiddleware<Middleware>();
    }

    public class Middleware(RequestDelegate next) : LoggingMiddleware(next)
    {
        protected override object GetErrorResponse(Exception exception)
        {
            if (exception is HttpErrorException<ErrorModel> ex)
                return new ErrorResponse(ex.Errors);

            return new ErrorResponse(new ErrorModel()
            {
                Code = ErrorCode.Unknown,
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
