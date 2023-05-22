namespace DFMS.WebApi.DataContracts.Permissions
{
    public class UpdatePermissionGroupInput
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? Active { get; set; }
    }
}
