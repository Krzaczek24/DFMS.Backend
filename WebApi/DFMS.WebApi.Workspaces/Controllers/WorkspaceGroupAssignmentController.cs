using AutoMapper;
using DFMS.Database.Services;
using DFMS.Database.Services.Workspace;
using DFMS.Shared.Exceptions;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Workspaces.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("workspace/group/{group-id}/user/{user-id}")]
    public class WorkspaceGroupAssignmentController(IMapper mapper) : ResponseController(mapper)
    {
        [HttpPost]
        public async Task Assign(
            [FromServices] IUserService userService,
            [FromServices] IWorkspaceGroupService workspaceGroupService,
            [FromServices] IWorkspaceGroupAssigmentService service,
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId)
        {
            try
            {
                if (!await userService.DoesUserExist(userId))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
                if (!await workspaceGroupService.DoesGroupExist(groupId))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);

                await service.AddGroupMember(User.GetLogin(), groupId, userId);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueRelation);
            }
        }

        [HttpDelete]
        public async Task Unassign(
            [FromServices] IWorkspaceGroupAssigmentService service,
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId)
        {
            try
            {
                if (!await service.RemoveGroupMember(groupId, userId))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.Unknown);
            }
        }
    }
}
