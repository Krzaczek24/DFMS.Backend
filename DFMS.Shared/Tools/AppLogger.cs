using NLog;
using System.Diagnostics;

namespace DFMS.Shared.Tools
{
    public class AppLogger
    {
        private ILogger _logger;
        public ILogger Logger => _logger ??= CreateLogger(null);

        public ILogger CreateLogger(string loggerName) => LogManager.GetLogger(loggerName ?? GetClassFullName());

        private string GetClassFullName()
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            return method.DeclaringType.FullName;
        }
    }
}
