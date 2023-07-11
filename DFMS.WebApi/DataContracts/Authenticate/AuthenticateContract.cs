using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Authenticate
{
    public class LogonInput
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;
    }

    public class RefreshInput
    {
        [Required]
        public string RefreshToken { get; set; } = default!;
    }

    public class AuthenticateOutput
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
