using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DFMS.WebApi
{
    public class Program
    {
        private static Logger Logger { get; set; }

        public static void Main(string[] args)
        {
            Logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

            try
            {
#if DEBUG
                const int frontendPort = 3000;
                if (GetProcessIdByPort(frontendPort).HasValue)
                {
                    Logger.Info($"> React Frontend already running on [{frontendPort}] port");
                }
                else
                {
                    Logger.Info("> Starting React Frontend");
                    RunFrontendProcess();
                }
#endif
                Logger.Info("> Starting WebApi");
                CreateHostBuilder(args).UseNLog().Build().Run();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Flush();
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(config => config.ClearProviders());
                    webBuilder.UseNLog();
                    webBuilder.UseStartup<Startup>();
                });

        private static int? GetProcessIdByPort(int port)
        {
            string? output;
            var proc = new Process()
            {
                StartInfo = new ProcessStartInfo("netstat", "-ano")
                {
                    RedirectStandardOutput = true
                }
            };

            proc.Start();
            while (!(output = proc.StandardOutput.ReadLine())?.Contains($"0.0.0.0:{port}") ?? false) { }
            proc.Kill();

            if (string.IsNullOrEmpty(output))
                return null;

            output = output.Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();

            if (int.TryParse(output, out int processId))
                return processId;

            return null;
        }

        private static void RunFrontendProcess()
        {
            var frontentStartBat = new FileInfo(@"..\..\DFMS.Frontend.TypeScriptReactWithMobx\run-frontend.bat");
            if (frontentStartBat.Exists)
            {
                new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = frontentStartBat.DirectoryName,
                        FileName = "cmd",
                        Arguments = $"/c {frontentStartBat.FullName}",
                        UseShellExecute = true,
                        CreateNoWindow = false,
                    }
                }.Start();
            }
        }
    }
}
