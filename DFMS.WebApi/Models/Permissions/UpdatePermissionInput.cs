using System.ComponentModel.DataAnnotations;

namespace DFMS.WebApi.Models.Permissions
{
    public class UpdatePermissionInput
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Active { get; set; }
    }
}
