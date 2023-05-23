using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models;

public class ApplicationRole : IdentityRole
{
    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }
}