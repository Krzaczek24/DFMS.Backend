using DFMS.Shared.Extensions;
using DFMS.WebApi.Common.Enums;
using DFMS.WebApi.Common.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace DFMS.WebApi.Common.Extensions
{
    public static class StartupExtensions
    {
        public static void AddDfmsJwtBearerAuthentication(this IServiceCollection services, byte[] key)
        {
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
        }

        public static IMvcBuilder ConfigureDfmsApiBehaviorOptions(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = CustomInvalidModelStateResponse);
        }

        public static BadRequestObjectResult CustomInvalidModelStateResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => new ErrorModel()
                {
                    Code = ErrorCode.InvalidRequestFieldValue,
                    Message = x.ErrorMessage
                }).ToList();

            return new BadRequestObjectResult(new ErrorResponse(errors));
        }
    }
}
