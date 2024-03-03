using AutoMapper;
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
    public class WorkspaceController(IMapper mapper) : ResponseController(mapper)
    {
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
