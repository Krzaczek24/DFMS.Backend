﻿using AutoMapper;
using DFMS.Database.Dto.Permissions;
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
    [ApiRoute("permission")]
    public class PermissionController(IMapper mapper, IPermissionService permissionService) : ResponseController(mapper)
    {
        private IPermissionService PermissionService { get; } = permissionService;

        [HttpGet("structure")]
        public async Task<PermissionGroupDto[]> GetPermissionsStructure()
        {
            return await PermissionService.GetPermissionsStructure();
        }

        [HttpPost]
        public async Task<int> CreatePermission([FromBody] CreatePermissionInput input)
        {
            try
            {
                return await PermissionService.CreatePermission(User.GetLogin(), input.Name, input.Description);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpPatch("{id}")]
        public async Task UpdatePermission([FromRoute] int id, [FromBody] UpdatePermissionInput input)
        {
            try
            {
                if (!await PermissionService.UpdatePermission(id, User.GetLogin(), input.Name, input.Description, input.Active))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpDelete("{id}")]
        public async Task RemovePermission([FromRoute] int id)
        {
            try
            {
                if (!await PermissionService.RemovePermission(id))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.ResourceInUse);
            }
        }
    }
}
