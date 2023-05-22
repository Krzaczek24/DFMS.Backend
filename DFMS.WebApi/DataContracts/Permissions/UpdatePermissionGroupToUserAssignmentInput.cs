using DFMS.Database.Tools;
using System;
using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Permissions
{
    public class UpdatePermissionGroupToUserAssignmentInput
    {
        [Required]
        public int PermissionId { get; set; }

        [Required]
        public int PermissionGroupId { get; set; }


        public bool? Active { get; set; }
        public Specifiable<DateTime?> ValidUntil { get; set; } = default!;
    }
}
