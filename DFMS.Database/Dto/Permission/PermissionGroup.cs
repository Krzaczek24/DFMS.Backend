namespace DFMS.Database.Dto.Permission
{
    public class PermissionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
