using AutoMapper;
using DFMS.Database.Dto.Users;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
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

        [HttpPost("/authenticate")]
        [AllowAnonymous]
        [Produces("text/plain")]
        public async Task<ActionResult<string>> Authenticate([FromBody] LogonInput input)
        {
            var user = await UserService.GetUser(input.Username, input.PasswordHash);
            if (user == null)
                return Unauthorized();

            string token = new TokenBuilder(Configuration[ConfigurationKeys.ApiKey], user).GenerateToken();
            return token;
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Register([FromBody] RegisterInput input)
        {
            var user = await UserService.CreateUser(input.Username, input.PasswordHash, input.FirstName, input.LastName);
            return user;
        }
    }
}