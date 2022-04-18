using DFMS.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DFMS.WebApi.Middlewares
{
    public static class RestLoggingMiddlewareHelper
    {
        public static IApplicationBuilder UseRestLoggingMiddleware(this IApplicationBuilder app) => app.UseMiddleware<LoggingMiddleware>();
    }

    public class LoggingMiddleware
    {
        private static ILogger Logger { get; } = LogManager.GetLogger(nameof(LoggingMiddleware));

        private RequestDelegate Next { get; }

        public LoggingMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Features.Set(Guid.NewGuid());

            await LogRequest(httpContext);
            await LogResponse(httpContext);
        }

        private async Task LogRequest(HttpContext httpContext)
        {
            var request = httpContext.Request;
            string bodyText = string.Empty;

            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyText = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            Logger.Info($"REQUEST  ({httpContext.GetGuid()}) | PATH ({request.GetPath()}) | BODY ({bodyText})");
        }

        private async Task LogResponse(HttpContext httpContext)
        {
            var response = httpContext.Response;
            var originalBodyStream = response.Body;
            var responseBody = new MemoryStream();

            response.Body = responseBody;

            try
            {
                await Next(httpContext);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, exception.Message);
                //response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //await response.WriteAsync(exception.Message);
            }

            response.Body.Seek(0, SeekOrigin.Begin);
            string bodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            Logger.Info($"RESPONSE ({httpContext.GetGuid()}) | CODE ({response.StatusCode}) | BODY ({bodyText})");

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
