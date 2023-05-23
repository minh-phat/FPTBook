using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
