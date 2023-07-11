using AutoMapper;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using DFMS.WebApi.Core.Errors;
using DFMS.WebApi.Core.Exceptions;
using DFMS.WebApi.DataContracts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(ControllerGroup.Api + "/permission-group")]
    public class PermissionGroupController : ResponseController
    {
        private IPermissionService PermissionService { get; }

        public PermissionGroupController(IMapper mapper, IPermissionService permissionService) : base(mapper)
        {
            PermissionService = permissionService;
        }

        #region Permission group
        [HttpPost]
        public async Task<int> CreateGroup([FromBody] AddPermissionGroupInput input)
        {
            try
            {
                return await PermissionService.CreatePermissionGroup(User.GetLogin(), input.Name, input.Description);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NON_UNIQUE_NAME);
            }
        }

        [HttpPatch("{id}")]
        public async Task UpdateGroup([FromRoute] int id, [FromBody] UpdatePermissionInput input)
        {
            try
            {
                if (!await PermissionService.UpdatePermissionGroup(id, User.GetLogin(), input.Name, input.Description, input.Active))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NON_UNIQUE_NAME);
            }
        }

        [HttpDelete("{id}")]
        public async Task RemoveGroup([FromRoute] int id)
        {
            try
            {
                if (!await PermissionService.RemovePermissionGroup(id))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.RESOURCE_IN_USE);
            }
        }
        #endregion

        #region Permission to group assignment
        [HttpPost("{group-id}/permission/{permission-id}")]
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
                throw new ConflictException(ErrorCode.NON_UNIQUE_RELATION);
            }
        }

        [HttpDelete("{group-id}/permission/{permission-id}")]
        public async Task RemovePermissionFromGroup(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "permission-id")] int permissionId)
        {
            try
            {
                if (!await PermissionService.RemovePermissionFromGroup(groupId, permissionId))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.UNKNOWN);
            }
        }
        #endregion

        #region User to group assignment
        [HttpPost("{group-id}/user/{user-id}")]
        public async Task AssignUserToPermissionGroup(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId,
            [FromQuery(Name = "valid-until")] DateTime? validUntil)
        {
            try
            {
                await PermissionService.AssignUserToPermissionGroup(User.GetLogin(), groupId, userId, validUntil);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NON_UNIQUE_RELATION);
            }
        }

        [HttpPatch("{group-id}/user/{user-id}")]
        public async Task UpdateUserPermissionGroupAssignment(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId,
            [FromQuery(Name = "valid-until")] DateTime? validUntil)
        {
            try
            {
                if (!await PermissionService.UpdateUserPermissionGroupAssignment(User.GetLogin(), groupId, userId, validUntil))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.UNKNOWN);
            }
        }

        [HttpDelete("{group-id}/user/{user-id}")]
        public async Task UnassignPermissionGroupFromUser(
            [FromRoute(Name = "group-id")] int groupId,
            [FromRoute(Name = "user-id")] int userId)
        {
            try
            {
                if (!await PermissionService.RemoveUserFromPermissionGroup(groupId, userId))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.UNKNOWN);
            }
        }
        #endregion
    }
}
