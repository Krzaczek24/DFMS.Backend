using System;

namespace DFMS.Shared.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetInnerExceptionMessage(this Exception ex) => ex.InnerException?.Message ?? ex.Message;
    }
}
