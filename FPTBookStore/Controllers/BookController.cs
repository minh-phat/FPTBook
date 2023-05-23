using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FPTBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Linq command to get all books from the context
        [Authorize(Roles = "Administrator, Manager")]
        public List<Book> ViewAllBooks()
        {
            var books = _context.Book.Include(b => b.Author).Include(b => b.Category).ToList();
            return books;
        }

        // GET: Book
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Index(string searchString)
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
        }

        // GET: Book/Details/5
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Details(int? id)
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

        // GET: Book/Create
        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Create([Bind("BookId,BookTitle,CategoryId,AuthorId,Pages,PublishDate,Description,Price,Available,Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName", book.CategoryId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,BookTitle,CategoryId,AuthorId,Pages,PublishDate,Description,Price,Available,Image")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Book/Delete/5
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
