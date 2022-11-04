using AutoMapper;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Models.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public async Task<IActionResult> AddPermission([FromBody] AddPermissionInput input)
        {
            int id = await PermissionService.AddPermission(User.GetLogin(), input.Name, input.Description);
            return Created(new Uri($"~/{Routes.Permission}/{id}"), input);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePermission([FromRoute] int id, [FromBody] UpdatePermissionInput input)
        {
            bool updated = await PermissionService.UpdatePermission(id, User.GetLogin(), input.Name, input.Description, input.Active) > 0;
            return updated ? Ok() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePermission([FromRoute] int id)
        {
            bool removed = await PermissionService.RemovePermission(id) > 0;
            return removed ? Ok() : NotFound();
        }

        [HttpGet("structure")]
        public async Task<IEnumerable<string>> GetPermissionsStructure()
        {
            return null;
        }
    }
}
