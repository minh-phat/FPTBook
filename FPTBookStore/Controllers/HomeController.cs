using BookstoreEmailService.Models;
using BookstoreEmailService.Services;
using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FPTBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var featured = _context.Book.Include(b => b.Author).Include(b => b.Category).Take(4).ToList();
            return View(featured);
        }

        public async Task<IActionResult> Single(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public async Task<IActionResult> Shop(string searchString)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }

            var books = from c in _context.Book
                        select c;

            //used to compare the input data with the data in the database, if the input data matches the data in the database, then get that data
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.BookTitle!.Contains(searchString));
            }

            var applicationDbContext = books.Include(b => b.Author).Include(b => b.Category);
            return View(await applicationDbContext.ToListAsync());

            //var books = _context.Book.Include(b => b.Author).Include(b => b.Category).ToList();
            //return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}