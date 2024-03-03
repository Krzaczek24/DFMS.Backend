using AutoMapper;
using DFMS.Database.Services;
using DFMS.Shared.Enums;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.DataContracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("user")]
    public class UserController : ResponseController
    {
        private IUserService UserService { get; }

        public UserController(IMapper mapper, IUserService userService) : base(mapper)
        {
            UserService = userService;
        }

        [DfmsAuthorize(UserRole.Admin)]
        [HttpGet("search")]
        public async Task<SearchUsersOutput> SearchUsers([FromQuery] SearchUsersInput input) => new()
        {
            TotalResults = await UserService.GetUsersCount(input.PhraseFilter, input.OnlineAfter, input.Active),
            Results = await UserService.SearchUsers(input.PhraseFilter, input.OnlineAfter, input.Active, input.Page, input.PageSize)
        };
    }
}
