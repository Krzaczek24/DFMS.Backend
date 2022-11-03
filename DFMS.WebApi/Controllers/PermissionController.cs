using AutoMapper;
using DFMS.Database.Services;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.Permission)]
    public class PermissionController : BaseController
    {
        private IPermissionService PermissionService { get; }

        public PermissionController(IMapper mapper, IPermissionService permissionService) : base(mapper)
        {
            PermissionService = permissionService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetPermissions()
        {
            return null;
        }
    }
}
