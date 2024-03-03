using AutoMapper;
using Core.WebApi.Extensions;
using DFMS.Database.Services;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute]
    public class LogoutController(IMapper mapper, IUserService userService) : ResponseController(mapper)
    {
        private IUserService UserService { get; } = userService;

        [HttpPost("logout")]
        public async Task Logout() => await Logout(HttpContext.GetClientIp());

        [HttpPost("logout-all-machines")]
        public async Task LogoutAllMachines() => await Logout(null);

        private async Task Logout(string? clientIp) => await UserService.RemoveRefreshTokens(User.GetLogin(), clientIp);
    }
}
