using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using App.ViewModels;

namespace App.Controllers
{
    public class ApplicationRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> roleManager;
        public ApplicationRolesController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _context = context;
            this.roleManager = roleManager;
        }

        // GET: ApplicationRoles
        public async Task<IActionResult> Index()
        {
              return _context.ApplicationRole != null ? 
                          View(await _context.ApplicationRole.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ApplicationRole'  is null.");
        }

        // GET: ApplicationRoles/Details/5
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

        // GET: ApplicationRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsEnabled,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationRole);
        }

        // GET: ApplicationRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ApplicationRole == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.ApplicationRole.FindAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }
            return View(applicationRole);
        }

        // POST: ApplicationRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IsEnabled,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationRoleExists(applicationRole.Id))
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
            return View(applicationRole);
        }

        // GET: ApplicationRoles/Delete/5
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

        // POST: ApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public async Task<IActionResult> ManageRoleClaims(string roleId)
        {
            //ViewBag.RoleId = roleId;    

            var role = await roleManager.FindByIdAsync(roleId);


            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var existingRoleClaims = await roleManager.GetClaimsAsync(role);

            var model = new RoleClaimViewModel
            {
                RoleId = roleId
            };

            //var data = claimsstore.allclaims;
            //var data_ = _context.Claims.ToListAsync();

            foreach (Claim claim in ClaimsStore.GetClaims(_context))
            {
                RoleClaims roleClaims = new RoleClaims
                {
                    ClaimType = claim.Type
                };

                if (existingRoleClaims.Any(c => c.Type == claim.Type))
                {
                    roleClaims.IsSelected = true;
                }
                model.Claims.Add(roleClaims);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoleClaims(RoleClaimViewModel model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            // Get all the user existing claims and delete them
            var claims = await roleManager.GetClaimsAsync(role);

            foreach (var claim in claims)
            {
                var result = await roleManager.RemoveClaimAsync(role, claim);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user existing claims");
                    return View(model);
                }


            }
            var data_ = model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType));

            foreach (var data in data_)
            {
                var result_ = await roleManager.AddClaimAsync(role, data);

                if (!result_.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected claims to user");
                    return View(model);
                }
            }
            return RedirectToAction("EditRole", new { Id = model.RoleId });


        }
    }
}
