using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Authorization.Models.DataContracts
{
    public class RegisterInput
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
    }
}
