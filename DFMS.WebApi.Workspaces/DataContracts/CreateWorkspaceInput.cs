using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Workspaces.DataContracts
{
    public class CreateWorkspaceInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsPublic { get; set; }
    }
}
