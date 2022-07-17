﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using App.Logic;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GLCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GLCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GLCategories
        public async Task<IActionResult> Index()
        {
              return _context.GLCategory != null ? 
                          View(await _context.GLCategory.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GLCategory'  is null.");
        }

        // GET: GLCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GLCategory == null)
            {
                return NotFound();
            }

            var gLCategory = await _context.GLCategory
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (gLCategory == null)
            {
                return NotFound();
            }

            return View(gLCategory);
        }

        // GET: GLCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GLCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,CodeNumber,IsEnabled,mainAccountCategory")] GLCategory gLCategory)
        {
            if (ModelState.IsValid)
            {
                GLCategoryLogic gLCategoryLogic = new();
                gLCategory.CodeNumber = gLCategoryLogic.GenerateGLCategoryCodeNumber(gLCategory.mainAccountCategory, gLCategory.CategoryId);

                _context.Add(gLCategory);
              
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gLCategory);
        }

        // GET: GLCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GLCategory == null)
            {
                return NotFound();
            }

            var gLCategory = await _context.GLCategory.FindAsync(id);
            if (gLCategory == null)
            {
                return NotFound();
            }
            return View(gLCategory);
        }

        // POST: GLCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDescription,CodeNumber,IsEnabled,mainAccountCategory")] GLCategory gLCategory)
        {
            if (id != gLCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gLCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GLCategoryExists(gLCategory.CategoryId))
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
            return View(gLCategory);
        }

        // GET: GLCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GLCategory == null)
            {
                return NotFound();
            }

            var gLCategory = await _context.GLCategory
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (gLCategory == null)
            {
                return NotFound();
            }

            return View(gLCategory);
        }

        // POST: GLCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GLCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GLCategory'  is null.");
            }
            var gLCategory = await _context.GLCategory.FindAsync(id);
            if (gLCategory != null)
            {
                _context.GLCategory.Remove(gLCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GLCategoryExists(int id)
        {
          return (_context.GLCategory?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
