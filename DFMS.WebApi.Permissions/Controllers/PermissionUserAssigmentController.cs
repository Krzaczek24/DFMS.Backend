using AutoMapper;
using DFMS.Database.Services.Permissions;
using DFMS.Shared.Exceptions;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DFMS.WebApi.Permissions.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("permission/group/{group-id}/user/{user-id}")]
    public class PermissionUserAssigmentController(IMapper mapper, IUserToGroupAssigmentService service) : ResponseController(mapper)
    {
        private IUserToGroupAssigmentService Service { get; } = service;

        [HttpPost]
        public async Task AssignUserToPermissionGroup(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId,
            [FromQuery(Name = "valid-until")] DateTime? validUntil)
        {
            try
            {
                await Service.AssignUserToPermissionGroup(User.GetLogin(), groupId, userId, validUntil);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueRelation);
            }
        }

        [HttpPatch]
        public async Task UpdateUserPermissionGroupAssignment(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId,
            [FromQuery(Name = "valid-until")] DateTime? validUntil)
        {
            try
            {
                if (!await Service.UpdateUserPermissionGroupAssignment(User.GetLogin(), groupId, userId, validUntil))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.Unknown);
            }
        }

        [HttpDelete]
        public async Task UnassignPermissionGroupFromUser(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId)
        {
            try
            {
                if (!await Service.RemoveUserFromPermissionGroup(groupId, userId))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.Unknown);
            }
        }
    }
}
