namespace DFMS.Database.Dto.Permissions
{
    public class PermissionGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public PermissionDto[] Permissions { get; set; }
    }
}
