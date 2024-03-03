using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Authorization.Models.DataContracts
{
    public class RefreshInput
    {
        [Required]
        public string RefreshToken { get; set; } = default!;
    }
}
