using AutoMapper;
using DFMS.Database.Dto.Workspace;
using DFMS.Database.Services.Workspace;
using DFMS.Shared.Exceptions;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using DFMS.WebApi.Workspaces.DataContracts.Workspace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Workspaces.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("workspace")]
    public class WorkspaceController(IWorkspaceService service, IMapper mapper) : ResponseController(mapper)
    {
        private IWorkspaceService Service { get; } = service;

        [HttpGet("{id}/structure")]
        public async Task<WorkspaceGroupDto[]> GetWorkspaceStructure([FromRoute] int id)
        {
            return await Service.GetWorkspaceStructure(id);
        }

        [HttpGet("list")]
        public async Task<WorkspaceDto[]> GetWorkspacesList()
        {
            return await Service.GetWorkspacesList();
        }

        [HttpPost]
        public async Task<int> CreateWorkspace([FromBody] CreateWorkspaceInput input)
        {
            try
            {
                return await Service.CreateWorkspace(User.GetLogin(), input.Name, input.Public);
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
                if (!await Service.UpdateWorkspace(id, User.GetLogin(), input.Name, input.Public))
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
            if (!await Service.UpdateWorkspace(id, User.GetLogin(), isActive: false))
                throw new NotFoundException(ErrorCode.ResourceNotFound);
        }
    }
}
