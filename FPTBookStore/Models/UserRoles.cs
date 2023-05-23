using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBookStore.Models
{
    public class UserRoles
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
