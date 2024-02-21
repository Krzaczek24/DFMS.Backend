using DFMS.Shared.Enums;
using System;

namespace DFMS.Database.Dto.Users
{
    public class User
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public UserRole Role { get; set; }
        public string[] Permissions { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstAndLastName => $"{FirstName} {LastName}".Trim();
        public string Name => string.IsNullOrEmpty(FirstAndLastName) ? Login : FirstAndLastName;
    }
}
