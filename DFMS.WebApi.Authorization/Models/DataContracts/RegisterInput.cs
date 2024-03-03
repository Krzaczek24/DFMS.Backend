using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Authorization.Models.DataContracts
{
    public class RegisterInput
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
    }
}
