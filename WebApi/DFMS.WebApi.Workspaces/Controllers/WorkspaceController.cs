using AutoMapper;
using DFMS.Database.Services.Workspace;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Workspaces.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Workspaces.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("workspace")]
    public class WorkspaceController(IWorkspaceService workspaceService, IMapper mapper) : ResponseController(mapper)
    {
        private IWorkspaceService WorkspaceService { get; } = workspaceService;

        [HttpPost]
        public void CreateWorkspace([FromBody] CreateWorkspaceInput input)
        {

        }

        [HttpPatch("{id}")]
        public void UpdateWorkspace([FromRoute] int id, [FromBody] UpdateWorkspaceInput input)
        {

        }

        [HttpDelete("{id}")]
        public void RemoveWorkspace([FromRoute] int id)
        {

        }
    }
}
