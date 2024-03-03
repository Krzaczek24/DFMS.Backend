using Core.WebApi.Extensions;
using DFMS.Database;
using DFMS.Shared.Converters;
using DFMS.WebApi.Common.Constants;
using DFMS.WebApi.Common.Extensions;
using DFMS.WebApi.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace DFMS.WebApi.Common.Startups
{
    public sealed class Startup(IConfiguration configuration, IStartupSettings settings)
    {
#if DEBUG
        private static readonly LoggerFactory LoggerFactory = new([new NLogLoggerProvider()]);
#endif
        private IConfiguration Configuration { get; } = configuration;
        private IStartupSettings Settings { get; } = settings;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // dotnet user-secrets init
            // dotnet user-secrets set ApiKey "<key>"
            // rmb project + 'manage user secrets'
            byte[] key = Encoding.ASCII.GetBytes(Configuration[ConfigurationKeys.ApiKey]!);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // dotnet user-secrets init
            // dotnet user-secrets set ConnectionStrings:DFMSdatabase "server=<server_address;port=<server_port>;user id=<username>;password=<pasword>;database=<scheme>"
            // rmb project + 'manage user secrets'
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString(ConfigurationKeys.DatabaseConnectionKey)!);
#if DEBUG
                options.UseLoggerFactory(LoggerFactory);
#endif
            });

            services.AddCoreScopedServices(typeof(AppDbContext).Assembly!);

            // `ReferenceHandler.IgnoreCycles` helps to handle cyclic references in entity framework core database models
            services.AddControllers().AddJsonOptions(opts => {
                opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opts.JsonSerializerOptions.Converters.Add(new EnumToStringConverterFactory());
            });

            services.AddMvc().ConfigureDfmsApiBehaviorOptions();
            services.AddSession();
            services.AddDfmsJwtBearerAuthentication(key);

            services.AddSwaggerGen(c => c.SwaggerDoc(Settings.Swagger?.Group, new OpenApiInfo { Title = Settings.Swagger?.Name, Version = "v1" }));

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{Settings.Swagger?.Group}/swagger.json", Settings.Swagger?.Name));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRestMiddleware();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
