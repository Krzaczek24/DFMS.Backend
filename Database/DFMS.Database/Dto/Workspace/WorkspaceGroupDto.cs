namespace DFMS.Database.Dto.Workspace
{
    public class WorkspaceGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WorkspaceGroupMemberDto[] Members { get; set; }
    }
}
