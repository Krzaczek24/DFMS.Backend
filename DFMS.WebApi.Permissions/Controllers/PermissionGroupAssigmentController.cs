using AutoMapper;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Permissions.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("permission/{permission-id}/group/{group-id}")]
    public class PermissionGroupAssigmentController(IMapper mapper, IPermissionService permissionService) : ResponseController(mapper)
    {
        private IPermissionService PermissionService { get; } = permissionService;

        [HttpPost]
        public async Task AddPermissionToGroup(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "permission-id")] int permissionId)
        {
            try
            {
                await PermissionService.AddPermissionToGroup(User.GetLogin(), groupId, permissionId);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueRelation);
            }
        }

        [HttpDelete]
        public async Task RemovePermissionFromGroup(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "permission-id")] int permissionId)
        {
            try
            {
                if (!await PermissionService.RemovePermissionFromGroup(groupId, permissionId))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.Unknown);
            }
        }
    }
}
