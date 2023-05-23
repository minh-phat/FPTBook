using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ICollection<CartDetails>? CartDetails { get; set; }
    }
}
