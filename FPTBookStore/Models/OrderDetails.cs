using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public Order? Order { get; set; }
        public Book? Book { get; set; }
    }
}
