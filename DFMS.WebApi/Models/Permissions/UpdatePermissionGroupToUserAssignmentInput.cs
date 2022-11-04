using DFMS.Database.Tools;
using System;
using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Models.Permissions
{
    public class UpdatePermissionGroupToUserAssignmentInput
    {
        [Required]
        public int PermissionId { get; set; }

        [Required]
        public int PermissionGroupId { get; set; }

        public Specifiable<DateTime?> ValidUntil { get; set; }

        public bool? Active { get; set; }
    }
}
