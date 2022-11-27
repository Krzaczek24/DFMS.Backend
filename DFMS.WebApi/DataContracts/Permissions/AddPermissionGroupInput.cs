﻿using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.DataContracts.Permissions
{
    public class AddPermissionGroupInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool? Active { get; set; }
    }
}