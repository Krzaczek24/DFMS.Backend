﻿using AutoMapper;
using DFMS.Database.Dto.Permission;
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
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(ControllerGroup.Api + "/permission")]
    public class PermissionController : ResponseController
    {
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

        [HttpPost]
        public async Task<int> CreatePermission([FromBody] AddPermissionInput input)
        {
            try
            {
                return await PermissionService.CreatePermission(User.GetLogin(), input.Name, input.Description);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NON_UNIQUE_NAME);
            }
        }

        [HttpPatch("{id}")]
        public async Task UpdatePermission([FromRoute] int id, [FromBody] UpdatePermissionInput input)
        {
            try
            {
                if (!await PermissionService.UpdatePermission(id, User.GetLogin(), input.Name, input.Description, input.Active))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NON_UNIQUE_NAME);
            }
        }

        [HttpDelete("{id}")]
        public async Task RemovePermission([FromRoute] int id)
        {
            try
            {
                if (!await PermissionService.RemovePermission(id))
                    throw new NotFoundException(ErrorCode.RESOURCE_NOT_FOUND);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.RESOURCE_IN_USE);
            }
        }
    }
}
