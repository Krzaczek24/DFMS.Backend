namespace DFMS.WebApi.Workspaces.DataContracts
{
    public class UpdateWorkspaceInput
    {
        public string? Name { get; set; }

        public bool? IsPublic { get; set; }

        public bool? IsActive { get; set; }
    }
}
