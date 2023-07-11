using AutoMapper;
using Core.WebApi.Extensions;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(ControllerGroup.Api)]
    public class LogoutController : ResponseController
    {
        private IUserService UserService { get; }

        public LogoutController(IMapper mapper, IUserService userService) : base(mapper)
        {
            UserService = userService;
        }

        [HttpPost("logout")]
        public async Task Logout() => await Logout(HttpContext.GetClientIp());

        [HttpPost("logout-all-machines")]
        public async Task LogoutAllMachines() => await Logout(null);

        private async Task Logout(string? clientIp) => await UserService.RemoveRefreshTokens(User.GetLogin(), clientIp);
    }
}
