using AutoMapper;
using DFMS.Database.Services.Workspace;
using DFMS.Shared.Exceptions;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Errors;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using DFMS.WebApi.Workspaces.DataContracts.WorkspaceGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Workspaces.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("workspace")]
    public class WorkspaceGroupController(IWorkspaceGroupService service, IMapper mapper) : ResponseController(mapper)
    {
        private IWorkspaceGroupService Service { get; } = service;

        [HttpPost("{id}/group")]
        public async Task<int> CreateWorkspaceGroup([FromRoute] int id,[FromBody] CreateWorkspaceGroupInput input)
        {
            try
            {
                return await Service.CreateGroup(id, User.GetLogin(), input.Name);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpPatch("group/{id}")]
        public async Task UpdateWorkspaceGroup([FromRoute] int id, [FromBody] UpdateWorkspaceGroupInput input)
        {
            try
            {
                if (!await Service.UpdateGroup(id, User.GetLogin(), input.Name))
                    throw new NotFoundException(ErrorCode.ResourceNotFound);
            }
            catch (DuplicatedEntryException)
            {
                throw new ConflictException(ErrorCode.NonUniqueName);
            }
        }

        [HttpDelete("group/{id}")]
        public async Task RemoveWorkspaceGroup([FromRoute] int id)
        {
            if (!await Service.RemoveGroup(id))
                throw new NotFoundException(ErrorCode.ResourceNotFound);
        }
    }
}
