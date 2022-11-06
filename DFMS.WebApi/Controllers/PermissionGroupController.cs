using AutoMapper;
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
    [Route(Routes.PermissionGroup)]
    public class PermissionGroupController : BaseController
    {
        private const string Assignment = "assignment";

        private IPermissionService PermissionService { get; }

        public PermissionGroupController(IMapper mapper, IPermissionService permissionService) : base(mapper)
        {
            PermissionService = permissionService;
        }

        #region Permission group
        [HttpPost]
        public async Task<int> AddPermissionGroup([FromBody] AddPermissionGroupInput input)
        {
            int id = await PermissionService.AddPermissionGroup(User.GetLogin(), input.Name, input.Description, input.Active);
            return id;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePermissionGroup([FromRoute] int id, [FromBody] UpdatePermissionGroupInput input)
        {
            bool updated = await PermissionService.UpdatePermissionGroup(id, User.GetLogin(), input.Name, input.Description, input.Active) > 0;
            return updated ? Ok() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePermissionGroup([FromRoute] int id)
        {
            bool removed = await PermissionService.RemovePermissionGroup(id) > 0;
            return removed ? Ok() : NotFound();
        }
        #endregion

        #region Permission group to user assignment
        [HttpPost(Assignment)]
        public async Task<int> AssignPermissionGroupToUser([FromBody] AssignPermissionGroupToUserInput input)
        {
            int id = await PermissionService.AssignPermissionGroupToUser(User.GetLogin(), input.PermissionId, input.PermissionGroupId, input.ValidUntil, input.Active);
            return id;
        }

        [HttpPatch(Assignment + "/{id}")]
        public async Task<IActionResult> UpdateUserPermissionGroupAssignment([FromRoute] int id, [FromBody] UpdatePermissionGroupToUserAssignmentInput input)
        {
            bool updated = await PermissionService.UpdateUserPermissionGroupAssignment(id, User.GetLogin(), input.ValidUntil, input.Active) > 0;
            return updated ? Ok() : NoContent();
        }

        [HttpDelete(Assignment + "/{id}")]
        public async Task<IActionResult> UnassignPermissionGroupFromUser([FromRoute] int id)
        {
            bool removed = await PermissionService.UnassignPermissionGroupFromUser(id) > 0;
            return removed ? Ok() : NotFound();
        }
        #endregion
    }
}
