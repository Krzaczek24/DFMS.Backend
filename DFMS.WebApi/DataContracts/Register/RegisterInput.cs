using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Register
{
    public class RegisterInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
