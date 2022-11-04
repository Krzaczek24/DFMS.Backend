using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Models.Permissions
{
    public class AddPermissionInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
