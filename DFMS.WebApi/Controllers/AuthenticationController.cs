using AutoMapper;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using DFMS.WebApi.Core.Errors;
using DFMS.WebApi.Core.Exceptions;
using DFMS.WebApi.DataContracts.Logon;
using DFMS.WebApi.DataContracts.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ResponseController
    {
        private IConfiguration Configuration { get; }
        private IUserService UserService { get; }

        public AuthenticationController(IMapper mapper, IUserService userService, IConfiguration configuration) : base(mapper)
        {
            Configuration = configuration;
            UserService = userService;
        }

        [HttpPost("/authenticate")]
        [AllowAnonymous]
        public async Task<LogonOutput> Authenticate([FromBody] LogonInput input)
        {
            if (!await UserService.AuthenticateUser(input.Username, input.PasswordHash))
                throw new UnauthorizedException();

            var user = await UserService.GetUser(input.Username);
            await UserService.UpdateLastLoginDate(input.Username);

            string token = new TokenBuilder(Configuration[ConfigurationKeys.ApiKey]!, user).GenerateToken();
            return new LogonOutput() { Token = token };
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task Register([FromBody] RegisterInput input)
        {
            try
            {
                _ = await UserService.CreateUser(input.Username, input.Username, input.PasswordHash, input.Email, input.FirstName, input.LastName);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.USERNAME_ALREADY_TAKEN);
            }
        }
    }
}