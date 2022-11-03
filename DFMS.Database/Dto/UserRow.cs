namespace DFMS.Database.Dto
{
    internal class UserRow
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Permission { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
