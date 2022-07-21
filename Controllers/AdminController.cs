using App.Data;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly RoleManager<ApplicationRole> roleManager;

        private readonly ApplicationDbContext _context;

       
        public AdminController(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this._context = context;  
        }

        //[Authorize]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = model.RoleName,
                    IsEnabled = model.IsEnabled,
                    NormalizedName = model.RoleName.ToUpper(),
                };

               

                if (ModelState.IsValid)
                {
                    _context.Add(applicationRole);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListRoles));
                }

          
            }

            return View(model);
        }

       // [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
           

            return _context.ApplicationRole != null ?
                      View(await _context.ApplicationRole.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.ApplicationRole'  is null.");
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewBag.RoleId = id;
          

            if (id == null || _context.ApplicationRole == null)
            {
                return NotFound();
            }
            var applicationRole = await _context.ApplicationRole.FindAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }

           

            var roleClaims = await roleManager.GetClaimsAsync(applicationRole);


            var model = new EditRoleViewModel
            {
                RoleName = applicationRole.Name,
                RoleId = applicationRole.Id,
                IsEnabled =  applicationRole.IsEnabled,
                Claims = roleClaims.Select(c => c.Value).ToList()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditRole(string id, EditRoleViewModel model)
        {
            
            var applicationRole = await _context.ApplicationRole.FindAsync(id);

            if (id != applicationRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                applicationRole.Name = model.RoleName;
                applicationRole.IsEnabled = model.IsEnabled;
                
                try
                {
                    _context.Update(applicationRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                    return NotFound();
                    
                   
                }
                return RedirectToAction(nameof(ListRoles));
            }

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> ManageRoleClaims(string roleId)
        {
            //ViewBag.RoleId = roleId;    

           

            var applicationRole = await _context.ApplicationRole.FindAsync(roleId);

            if (applicationRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

           

            var existingClaims = await roleManager.GetClaimsAsync(applicationRole);


            var model = new RoleClaimViewModel
            {
                RoleId = roleId
            };


            foreach (Claim claim in ClaimsStore.GetClaims(_context))
            {
                RoleClaims roleClaims = new RoleClaims
                {
                    ClaimType = claim.Type
                };

         

                if (existingClaims.Any(c => c.Type == claim.Type))
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


            var applicationRole = await _context.ApplicationRole.FindAsync(roleId);



            if (applicationRole == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {roleId} cannot be found";
                return View("NotFound");
            }




            var claims = await roleManager.GetClaimsAsync(applicationRole);

            foreach (var claim in claims)
            {
                var result = await roleManager.RemoveClaimAsync(applicationRole, claim);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user existing claims");
                    return View(model);
                }


            }
            var data_ =  model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType));
               
              foreach(var data in data_) 
            { 
                var result_ = await roleManager.AddClaimAsync(applicationRole, data);

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
