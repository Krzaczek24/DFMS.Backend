using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Workspaces.DataContracts.WorkspaceGroup
{
    public class UpdateWorkspaceGroupInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
