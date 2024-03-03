using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Authorization.Models.DataContracts
{
    public class LogonInput
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;
    }
}
