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
    public class RoleEnabledsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleEnabledsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoleEnableds
        public async Task<IActionResult> Index()
        {
              return _context.RoleEnabled != null ? 
                          View(await _context.RoleEnabled.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RoleEnabled'  is null.");
        }

        // GET: RoleEnableds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoleEnabled == null)
            {
                return NotFound();
            }

            var roleEnabled = await _context.RoleEnabled
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleEnabled == null)
            {
                return NotFound();
            }

            return View(roleEnabled);
        }

        // GET: RoleEnableds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleEnableds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AspNetRolesId,IsEnabled")] RoleEnabled roleEnabled)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleEnabled);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleEnabled);
        }

        // GET: RoleEnableds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoleEnabled == null)
            {
                return NotFound();
            }

            var roleEnabled = await _context.RoleEnabled.FindAsync(id);
            if (roleEnabled == null)
            {
                return NotFound();
            }
            return View(roleEnabled);
        }

        // POST: RoleEnableds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AspNetRolesId,IsEnabled")] RoleEnabled roleEnabled)
        {
            if (id != roleEnabled.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleEnabled);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleEnabledExists(roleEnabled.Id))
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
            return View(roleEnabled);
        }

        // GET: RoleEnableds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoleEnabled == null)
            {
                return NotFound();
            }

            var roleEnabled = await _context.RoleEnabled
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleEnabled == null)
            {
                return NotFound();
            }

            return View(roleEnabled);
        }

        // POST: RoleEnableds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoleEnabled == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoleEnabled'  is null.");
            }
            var roleEnabled = await _context.RoleEnabled.FindAsync(id);
            if (roleEnabled != null)
            {
                _context.RoleEnabled.Remove(roleEnabled);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleEnabledExists(int id)
        {
          return (_context.RoleEnabled?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
