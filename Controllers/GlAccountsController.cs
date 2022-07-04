using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;

namespace App.Controllers
{
    public class GlAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GlAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GlAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GlAccount.Include(g => g.Branch).Include(g => g.GlCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GlAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GlAccount == null)
            {
                return NotFound();
            }

            var glAccount = await _context.GlAccount
                .Include(g => g.Branch)
                .Include(g => g.GlCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (glAccount == null)
            {
                return NotFound();
            }

            return View(glAccount);
        }

        // GET: GlAccounts/Create
        public IActionResult Create()
        {
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address");
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription");
            return View();
        }

        // POST: GlAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountName,CodeNumber,AccountBalance,GLCategoryID,BranchID")] GlAccount glAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", glAccount.BranchID);
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription", glAccount.GLCategoryID);
            return View(glAccount);
        }

        // GET: GlAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GlAccount == null)
            {
                return NotFound();
            }

            var glAccount = await _context.GlAccount.FindAsync(id);
            if (glAccount == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", glAccount.BranchID);
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription", glAccount.GLCategoryID);
            return View(glAccount);
        }

        // POST: GlAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountName,CodeNumber,AccountBalance,GLCategoryID,BranchID")] GlAccount glAccount)
        {
            if (id != glAccount.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlAccountExists(glAccount.ID))
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
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", glAccount.BranchID);
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription", glAccount.GLCategoryID);
            return View(glAccount);
        }

        // GET: GlAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GlAccount == null)
            {
                return NotFound();
            }

            var glAccount = await _context.GlAccount
                .Include(g => g.Branch)
                .Include(g => g.GlCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (glAccount == null)
            {
                return NotFound();
            }

            return View(glAccount);
        }

        // POST: GlAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GlAccount == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GlAccount'  is null.");
            }
            var glAccount = await _context.GlAccount.FindAsync(id);
            if (glAccount != null)
            {
                _context.GlAccount.Remove(glAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlAccountExists(int id)
        {
          return (_context.GlAccount?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
