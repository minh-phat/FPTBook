
using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class CartDetails
    {
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        private int _subTotal;
        public int SubTotal
        {
            get => _subTotal;
            set => _subTotal = (int)(Book.Price * Quantity);
        }
        public Cart? Cart { get; set; }
        public Book? Book { get; set; }
    }
}
