namespace DFMS.WebApi.Permissions.DataContracts
{
    public class UpdatePermissionInput
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? Active { get; set; }
    }
}
