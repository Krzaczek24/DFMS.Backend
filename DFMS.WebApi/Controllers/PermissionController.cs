using AutoMapper;
using DFMS.Database.Dto.Permission;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.DataContracts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.Permission)]
    public class PermissionController : BaseController
    {
        private const string Assignment = "assignment";

        private IPermissionService PermissionService { get; }

        public PermissionController(IMapper mapper, IPermissionService permissionService) : base(mapper)
        {
            PermissionService = permissionService;
        }

        [HttpGet("structure")]
        public async Task<PermissionGroup[]> GetPermissionsStructure()
        {
            return await PermissionService.GetPermissionsStructure();
        }

        #region Permission
        [HttpPost]
        public async Task<int> AddPermission([FromBody] AddPermissionInput input)
        {
            return await PermissionService.AddPermission(User.GetLogin(), input.Name, input.Description, input.Active);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePermission([FromRoute] int id, [FromBody] UpdatePermissionInput input)
        {
            bool updated = await PermissionService.UpdatePermission(id, User.GetLogin(), input.Name, input.Description, input.Active);
            return updated ? Ok() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePermission([FromRoute] int id)
        {
            //bool removed = await PermissionService.RemovePermission(id) > 0;
            //return removed ? Ok() : NotFound();

            return Ok();
        }
        #endregion

        #region Permission to group assignment
        [HttpPost(Assignment)]
        public async Task<int> AssignPermissionToGroup([FromBody] AssignPermissionToGroupInput input)
        {
            //int id = await PermissionService.AssignPermissionToGroup(User.GetLogin(), input.PermissionId, input.PermissionGroupId, input.Active);
            //return id;

            return 0;
        }

        [HttpDelete(Assignment + "/{id}")]
        public async Task<IActionResult> UnassignPermissionFromGroup([FromRoute] int id)
        {
            //bool removed = await PermissionService.UnassignPermissionFromGroup(id) > 0;
            //return removed ? Ok() : NotFound();

            return Ok();
        }
        #endregion
    }
}
