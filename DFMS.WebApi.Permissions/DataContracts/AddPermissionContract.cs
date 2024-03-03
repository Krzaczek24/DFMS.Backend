﻿using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Permissions.DataContracts
{
    public class CreatePermissionInput
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
