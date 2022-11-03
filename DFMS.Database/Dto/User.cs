namespace DFMS.Database.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string[] Privileges { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
