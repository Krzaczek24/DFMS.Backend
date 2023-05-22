using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Permissions
{
    public class AddPermissionGroupInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public bool? Active { get; set; }
    }
}
