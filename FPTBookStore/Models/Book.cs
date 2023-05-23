using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBookStore.Models
{
    public class Book
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        [Required]
        [ForeignKey("Author")]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }

        [Required]
        [Display(Name = "Pages")]
        public int Pages { get; set; }

        [Required]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Available")]
        public int Available { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
