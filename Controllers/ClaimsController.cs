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
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
              return _context.Claims != null ? 
                          View(await _context.Claims.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Claims'  is null.");
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims
                .FirstOrDefaultAsync(m => m.ClaimsId == id);
            if (claims == null)
            {
                return NotFound();
            }

            return View(claims);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimsId,ClaimsName")] Claims claims)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claims);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claims);
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims.FindAsync(id);
            if (claims == null)
            {
                return NotFound();
            }
            return View(claims);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimsId,ClaimsName")] Claims claims)
        {
            if (id != claims.ClaimsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claims);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimsExists(claims.ClaimsId))
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
            return View(claims);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims
                .FirstOrDefaultAsync(m => m.ClaimsId == id);
            if (claims == null)
            {
                return NotFound();
            }

            return View(claims);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Claims == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Claims'  is null.");
            }
            var claims = await _context.Claims.FindAsync(id);
            if (claims != null)
            {
                _context.Claims.Remove(claims);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimsExists(int id)
        {
          return (_context.Claims?.Any(e => e.ClaimsId == id)).GetValueOrDefault();
        }
    }
}
