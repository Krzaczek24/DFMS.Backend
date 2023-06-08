using Core.WebApi.Extensions;
using Core.WebApi.Middlewares;
using DFMS.Database;
using DFMS.Shared.Extensions;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Errors;
using DFMS.WebApi.Core.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DFMS.WebApi
{
    public class Startup
    {
#if DEBUG
        private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new NLogLoggerProvider() });
#endif
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddMvc().ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = CustomInvalidModelStateResponse);

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.MapInboundClaims = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = UserClaim.Role.ToCamelCase(),
                    NameClaimType = UserClaim.Name.ToCamelCase()
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DFMS.WebApi", Version = "v1" });
            });

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DFMS.WebApi v1"));
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
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public static BadRequestObjectResult CustomInvalidModelStateResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => new ErrorModel() {
                    Code = ErrorCode.INVALID_REQUEST_FIELD_VALUE,
                    Message = x.ErrorMessage
                }).ToList();

            return new BadRequestObjectResult(new ErrorResponse(errors));
        }
    }
}
