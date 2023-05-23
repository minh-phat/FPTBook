using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Author
    {
        [Required]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Name")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
