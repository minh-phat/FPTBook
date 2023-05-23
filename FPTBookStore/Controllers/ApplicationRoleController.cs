using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBookStore.Data;
using FPTBookStore.Models;
using FPTBookStore.Data.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace FPTBookStore.Controllers
{
    public class ApplicationRoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationRole
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
              return _context.ApplicationRole != null ? 
                          View(await _context.ApplicationRole.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ApplicationRole'  is null.");
        }

        // GET: ApplicationRole/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ApplicationRole == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.ApplicationRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        // GET: ApplicationRole/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                applicationRole.NormalizedName = applicationRole.Name;
                _context.Add(applicationRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationRole);
        }

        // GET: ApplicationRole/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ApplicationRole == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.ApplicationRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        // POST: ApplicationRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ApplicationRole == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicationRole'  is null.");
            }
            var applicationRole = await _context.ApplicationRole.FindAsync(id);
            if (applicationRole != null)
            {
                _context.ApplicationRole.Remove(applicationRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationRoleExists(string id)
        {
          return (_context.ApplicationRole?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
