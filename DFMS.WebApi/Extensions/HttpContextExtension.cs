using Microsoft.AspNetCore.Http;
using System;

namespace DFMS.WebApi.Extensions
{
    public static class HttpContextExtension
    {
        public static void SetGuid(this HttpContext context, Guid guid) => context.Features.Set(guid);
        public static Guid GetGuid(this HttpContext context) => context.Features.Get<Guid>();

        public static string GetPath(this HttpRequest request) => $"{request.Method} {request.Path}{request.QueryString}";
    }
}
