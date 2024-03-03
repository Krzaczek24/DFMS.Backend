using AutoMapper;
using DFMS.Database.Services.Permissions;
using DFMS.Shared.Exceptions;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using DFMS.WebApi.Permissions.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Permissions.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("permission/group")]
    public class PermissionGroupController(IMapper mapper, IPermissionGroupService service) : ResponseController(mapper)
    {
        private IPermissionGroupService Service { get; } = service;

        [HttpPost]
        public async Task<int> CreateGroup([FromBody] AddPermissionGroupInput input)
        {
            try
            {
                return await Service.CreatePermissionGroup(User.GetLogin(), input.Name, input.Description);
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
                if (!await Service.UpdatePermissionGroup(id, User.GetLogin(), input.Name, input.Description, input.Active))
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
                if (!await Service.RemovePermissionGroup(id))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.ResourceInUse);
            }
        }
    }
}
