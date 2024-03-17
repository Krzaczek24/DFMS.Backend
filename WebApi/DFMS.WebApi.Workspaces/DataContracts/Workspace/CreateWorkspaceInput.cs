using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Workspaces.DataContracts.Workspace
{
    public class CreateWorkspaceInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool Public { get; set; }
    }
}
