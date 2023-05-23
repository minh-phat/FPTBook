using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBookStore.Controllers
{
    public class OrderController : Controller
    {
        internal readonly ApplicationDbContext _context;
        internal readonly IHttpContextAccessor _httpContextAccessor;
        internal readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult UserOrders()
        {
            var userId = GetUserId();

            var orders = _context.Order
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Category)
                .Where(o => o.UserId == userId)
                .ToList();

            return View(orders);
        }

        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult GetOrder()
        {
            var orders = _context.Order.ToList();
            return View(orders);
        }

        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult ShipOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            order.OrderStatus = "Shipping";
            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction("GetOrder");
        }

        [Authorize]
        public IActionResult ConfirmReceive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            order.OrderStatus = "Received";
            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction("UserOrders");
        }

        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Delete(int id)
        {
            var order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Order.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("GetOrder");
        }

        private string GetUserId()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return userId;
        }
    }
}
