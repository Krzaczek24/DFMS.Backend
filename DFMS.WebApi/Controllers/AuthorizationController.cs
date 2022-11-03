using AutoMapper;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController : BaseController
    {
        private IConfiguration Configuration { get; }
        private IUserService UserService { get; }

        public AuthorizationController(IMapper mapper, IUserService userService, IConfiguration configuration) : base(mapper)
        {
            Configuration = configuration;
            UserService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize([FromBody] LogonInput input)
        {
            var user = await UserService.GetUser(input.Login, input.PasswordHash);
            if (user == null)
                return Unauthorized();

            string token = new TokenBuilder(Configuration[ConfigurationKeys.ApiKey], user).GetToken();
            return Ok(new { user, token });
        }
    }
}