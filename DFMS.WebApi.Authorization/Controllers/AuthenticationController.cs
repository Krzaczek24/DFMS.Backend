using AutoMapper;
using Core.WebApi.Extensions;
using DFMS.Database.Dto.Users;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization.Models.DataContracts;
using DFMS.WebApi.Authorization.Token;
using DFMS.WebApi.Common.Constants;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route(ControllerGroup.Auth)]
    [ApiExplorerSettings(GroupName = ControllerGroup.Auth)]
    public class AuthenticationController : ResponseController
    {
        private IConfiguration Configuration { get; }
        private IUserService UserService { get; }

        public AuthenticationController(IMapper mapper, IUserService userService, IConfiguration configuration) : base(mapper)
        {
            Configuration = configuration;
            UserService = userService;
        }

        [HttpPost("login")]
        public async Task<AuthenticateOutput> Authenticate([FromBody] LogonInput input)
        {
            if (!await UserService.AuthenticateUser(input.Username, input.PasswordHash))
                throw new UnauthorizedException();

            await UserService.UpdateLastLoginDate(input.Username);
            var user = await UserService.GetUser(input.Username);

            try
            {
                return await GenerateTokens(user, UserService.SaveNewRefreshToken);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.TokenExists);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<AuthenticateOutput> RefreshToken([FromBody] RefreshInput input)
        {
            if (!TokenBuilder.IsRefreshTokenValid(input.RefreshToken))
                throw new UnauthorizedException(ErrorCode.TokenExpired);

            var user = await UserService.GetUser(input.RefreshToken, HttpContext.GetClientIp());
            if (user == null)
                throw new UnauthorizedException(ErrorCode.TokenInvalid);

            return await GenerateTokens(user, UserService.UpdateRefreshToken);
        }

        [HttpPost("register")]
        public async Task Register([FromBody] RegisterInput input)
        {
            try
            {
                _ = await UserService.CreateUser(input.Username, input.Username, input.PasswordHash, input.Email, input.FirstName, input.LastName);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.UsernameAlreadyTaken);
            }
        }

        private async Task<AuthenticateOutput> GenerateTokens(User user, Func<string, string?, string, DateTime?, Task> refreshTokenFunc)
        {
            var tokenBuilder = new TokenBuilder(Configuration[Common.Constants.ConfigurationKeys.ApiKey]!);
            string refreshToken = tokenBuilder.GenerateRefreshToken(out DateTime? validUntil);
            var saveRefreshTokenTask = refreshTokenFunc(user.Login, HttpContext.GetClientIp(), refreshToken, validUntil);
            string accessToken = tokenBuilder.GenerateAccessToken(user);
            await saveRefreshTokenTask;

            return new AuthenticateOutput()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}