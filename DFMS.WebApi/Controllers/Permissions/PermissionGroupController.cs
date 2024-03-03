using AutoMapper;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using DFMS.WebApi.DataContracts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers.Permissions
{
    [Authorize]
    [ApiController]
    [ApiRoute("permission/group")]
    public class PermissionGroupController(IMapper mapper, IPermissionService permissionService) : ResponseController(mapper)
    {
        private IPermissionService PermissionService { get; } = permissionService;

        [HttpPost]
        public async Task<int> CreateGroup([FromBody] AddPermissionGroupInput input)
        {
            try
            {
                return await PermissionService.CreatePermissionGroup(User.GetLogin(), input.Name, input.Description);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpPatch("{id}")]
        public async Task UpdateGroup([FromRoute] int id, [FromBody] UpdatePermissionInput input)
        {
            try
            {
                if (!await PermissionService.UpdatePermissionGroup(id, User.GetLogin(), input.Name, input.Description, input.Active))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpDelete("{id}")]
        public async Task RemoveGroup([FromRoute] int id)
        {
            try
            {
                if (!await PermissionService.RemovePermissionGroup(id))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.ResourceInUse);
            }
        }
    }
}
