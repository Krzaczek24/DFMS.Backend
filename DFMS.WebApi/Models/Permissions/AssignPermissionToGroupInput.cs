using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Models.Permissions
{
    public class AssignPermissionToGroupInput
    {
        [Required]
        public int PermissionId { get; set; }

        [Required]
        public int PermissionGroupId { get; set; }

        public bool? Active { get; set; }
    }
}
