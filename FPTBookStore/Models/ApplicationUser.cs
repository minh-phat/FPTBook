using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get; set; }
    }
}
