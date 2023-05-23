using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBookStore.Controllers
{
    public class CartController : Controller
    {

        internal readonly ApplicationDbContext _context;
        internal readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return _userManager.GetUserId(user);
        }

        [Authorize]
        public IActionResult GetUserCart()
        {
            var userID = GetUserId() ?? throw new Exception("Invalid UserId");
            var cart = _context.Cart
                .Include(x => x.CartDetails)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.CartDetails)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.UserId == userID);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userID
                };
                _context.Cart.Add(cart);
                _context.SaveChanges();
                return View(cart);
            }

            return View(cart);
        }

        [Authorize]
        public Cart GetCart(string userId)
        {
            var cart = _context.Cart.FirstOrDefault(x => x.UserId == userId);
            return cart;
        }

        [Authorize]
        public IActionResult AddItem(int bookID, int quantity = 1)
        {
            var userID = GetUserId();
            var cart = GetCart(userID);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userID
                };
                _context.Cart.Add(cart);
                _context.SaveChanges();
            }

            var cartDetails = _context.CartDetails.FirstOrDefault(x => x.CartId == cart.Id && x.BookId == bookID);
            if (cartDetails == null)
            {
                if (_context.Book.FirstOrDefault(x => x.BookId == bookID)?.Available == 0)
                {
                    return RedirectToAction("GetUserCart");
                }
                cartDetails = new CartDetails
                {
                    CartId = cart.Id,
                    BookId = bookID,
                    Quantity = quantity
                };
                _context.CartDetails.Add(cartDetails);
            }
            else if (_context.Book.FirstOrDefault(x => x.BookId == bookID)?.Available >= cartDetails.Quantity + 1)
            {
                cartDetails.Quantity += quantity;
            }
            _context.SaveChanges();

            return RedirectToAction("GetUserCart");
        }

        [Authorize]
        public IActionResult RemoveItem(int bookID)
        {
            var userID = GetUserId();
            var cart = GetCart(userID);
            var cartDetails = _context.CartDetails.FirstOrDefault(x => x.CartId == cart.Id && x.BookId == bookID);

            if (cartDetails != null && cart != null)
            {
                if (cartDetails.Quantity == 1)
                {
                    _context.CartDetails.Remove(cartDetails);
                }
                else
                {
                    cartDetails.Quantity--;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("GetUserCart");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CheckoutCart(string address, string phone)
        {
            var userID = GetUserId();
            var cart = GetCart(userID);
            var cartDetails = _context.CartDetails.Where(x => x.CartId == cart.Id).ToList();

            if (cartDetails.Count == 0)
            {
                return RedirectToAction("GetUserCart");
            }

            var order = new Order
            {
                UserId = userID,
                Address = address,
                Phone = phone,
                OrderStatus = "Pending",
            };

            _context.Order.Add(order);
            _context.SaveChanges();

            foreach (var item in cartDetails)
            {
                var book = _context.Book.FirstOrDefault(x => x.BookId == item.BookId);

                var orderDetails = new OrderDetails
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = (double)item.Book.Price
                };
                _context.OrderDetails.Add(orderDetails);

                if (book != null)
                {
                    book.Available -= item.Quantity;
                }
            }
            _context.SaveChanges();

            _context.CartDetails.RemoveRange(cartDetails);
            _context.SaveChanges();

            return RedirectToAction("UserOrders", "Order");
        }

    }
}
