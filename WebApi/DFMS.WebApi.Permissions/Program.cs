using DFMS.WebApi.Common.Constants;
using DFMS.WebApi.Common.Startups;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

namespace DFMS.WebApi.Permissions
{
    public class Program
    {
        private const string ModuleName = "Permissions";
        private static Logger? Logger { get; set; }

        public static void Main(string[] args)
        {
            Logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

            try
            {
                Logger.Info($"> Starting WebApi.{ModuleName}");
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

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureLogging(config => config.ClearProviders());
                webBuilder.UseNLog();
                webBuilder.UseStartup(builder => new Startup(builder.Configuration, new StartupSettings()
                {
                    Swagger = new SwaggerSettings(ControllerGroup.Api, $"DFMS.{ModuleName}")
                }));
            });
    }
}
