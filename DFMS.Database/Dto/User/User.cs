namespace DFMS.Database.Dto.User
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string[] Permissions { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
