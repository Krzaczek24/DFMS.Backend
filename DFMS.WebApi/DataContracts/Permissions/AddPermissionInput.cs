﻿using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Permissions
{
    public class AddPermissionInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
