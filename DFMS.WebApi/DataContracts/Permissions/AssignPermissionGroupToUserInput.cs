using System;
using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Permissions
{
    public class AssignPermissionGroupToUserInput
    {
        [Required]
        public int PermissionId { get; set; }

        [Required]
        public int PermissionGroupId { get; set; }

        public DateTime? ValidUntil { get; set; }

        public bool? Active { get; set; }
    }
}
