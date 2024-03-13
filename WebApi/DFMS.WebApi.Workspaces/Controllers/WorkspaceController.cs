using AutoMapper;
using DFMS.Database.Services.Permissions;
using DFMS.Database.Services.Workspace;
using DFMS.Shared.Exceptions;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using DFMS.WebApi.Workspaces.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Workspaces.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("workspace")]
    public class WorkspaceController(IWorkspaceService workspaceService, IMapper mapper) : ResponseController(mapper)
    {
        private IWorkspaceService WorkspaceService { get; } = workspaceService;

        [HttpPost]
        public async Task<int> CreateWorkspace([FromBody] CreateWorkspaceInput input)
        {
            try
            {
                return await WorkspaceService.CreateWorkspace(User.GetLogin(), input.Name, input.IsPublic);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpPatch("{id}")]
        public async Task UpdateWorkspace([FromRoute] int id, [FromBody] UpdateWorkspaceInput input)
        {
            try
            {
                if (!await WorkspaceService.UpdateWorkspace(id, User.GetLogin(), input.Name, input.IsPublic, input.IsActive))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpDelete("{id}")]
        public async Task RemoveWorkspace([FromRoute] int id)
        {
            try
            {
                if (!await WorkspaceService.RemoveWorkspace(id))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                throw new ConflictException(ErrorCode.ResourceInUse);
            }
        }
    }
}
