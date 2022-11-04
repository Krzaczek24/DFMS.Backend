namespace DFMS.WebApi.Models.Permissions
{
    public class UpdatePermissionGroupInput
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Active { get; set; }
    }
}
