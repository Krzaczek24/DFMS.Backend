using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Models
{
    public class LogonInput
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
