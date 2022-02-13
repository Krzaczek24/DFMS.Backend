using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace DFMS.Database
{
    public class Interceptor : DbCommandInterceptor
    {
        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            base.CommandFailed(command, eventData);
            LogInfo("CommandFailed", eventData.ToString(), command.CommandText);
        }

        public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            var task = base.CommandFailedAsync(command, eventData, cancellationToken);
            LogInfo("CommandFailedAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            base.NonQueryExecuted(command, eventData, result);
            LogInfo("NonQueryExecuted", eventData.ToString(), command.CommandText);
            return result;
        }

        public override ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var task = base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
            LogInfo("NonQueryExecutedAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            base.NonQueryExecuting(command, eventData, result);
            LogInfo("NonQueryExecutedAsync", eventData.ToString(), command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var task = base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
            LogInfo("NonQueryExecutedAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            base.ReaderExecuted(command, eventData, result);
            LogInfo("ReaderExecuted", eventData.ToString(), command.CommandText);
            return result;
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            var task = base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
            LogInfo("ReaderExecutedAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            base.ReaderExecuting(command, eventData, result);
            LogInfo("ReaderExecuting", eventData.ToString(), command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            var task = base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
            LogInfo("ReaderExecutingAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            base.ScalarExecuted(command, eventData, result);
            LogInfo("ScalarExecuted", eventData.ToString(), command.CommandText);
            return result;
        }

        public override ValueTask<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object result, CancellationToken cancellationToken = default)
        {
            var task = base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
            LogInfo("ScalarExecutedAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            base.ScalarExecuting(command, eventData, result);
            LogInfo("ScalarExecuting", eventData.ToString(), command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            var task = base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
            LogInfo("ScalarExecutingAsync", eventData.ToString(), command.CommandText);
            return task;
        }

        private void LogInfo(string method, string command, string commandText)
        {
            Console.WriteLine("Intercepted on: {0} \n {1} \n {2}", method, command, commandText);
        }
    }
}
