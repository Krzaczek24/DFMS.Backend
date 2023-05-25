using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.Database.Dto.Users;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.Shared.Constants;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Constants.Enums.Responses.Results;
using DFMS.WebApi.DataContracts;
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
    public class AuthenticationController : BaseController
    {
        private IConfiguration Configuration { get; }
        private IUserService UserService { get; }

        public AuthenticationController(IMapper mapper, IUserService userService, IConfiguration configuration) : base(mapper)
        {
            Configuration = configuration;
            UserService = userService;
        }

        [Produces("text/plain")]
        [HttpPost("/authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Authenticate([FromBody] LogonInput input)
        {
            if (!await UserService.AuthenticateUser(input.Username, input.PasswordHash))
                return Unauthorized();

            var user = await UserService.GetUser(input.Username);
            await UserService.UpdateLastLoginDate(input.Username);

            string token = new TokenBuilder(Configuration[ConfigurationKeys.ApiKey]!, user).GenerateToken();
            return token;
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<ApiResponse<RegistrationResult>> Register([FromBody] RegisterInput input)
        {
            try
            {
                _ = await UserService.CreateUser(input.Username, input.Username, input.PasswordHash, input.Email, input.FirstName, input.LastName);
            }
            catch (DuplicatedEntryException)
            {
                return ApiResponse.Failure.WithResult(RegistrationResult.UsernameAlreadyTaken);
            }
            catch
            {
                return ApiResponse.Failure.WithResult(RegistrationResult.Failure);
            }
            
            return ApiResponse.Success.As<RegistrationResult>();
        }
    }
}