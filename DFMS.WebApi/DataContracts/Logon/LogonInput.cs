using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Logon
{
    public class LogonInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
