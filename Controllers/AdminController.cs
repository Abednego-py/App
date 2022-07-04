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
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly ApplicationDbContext _context;

        //private readonly IdentityRoleClaim<ClaimsIdentity> claimsManger;

        //private readonly IdentityRoleClaim<IdentityRole> roleClaim;
        public AdminController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this._context = context;  
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var roles = await roleManager.Roles.ToListAsync();

            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewBag.RoleId = id;
            var role = await roleManager.FindByIdAsync(id);
            var roleEnabled = _context.RoleEnabled.Where(c => id == c.Id.ToString()).FirstOrDefault();

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var roleClaims = await roleManager.GetClaimsAsync(role);

            var model = new EditRoleViewModel
            {
                RoleName = role.Name,
                RoleId = role.Id,
                IsEnabled = roleEnabled.IsEnabled,
                Claims = roleClaims.Select(c => c.Value).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            var roleEnabled = _context.RoleEnabled.Where(c => id == c.Id.ToString()).FirstOrDefault();
            roleEnabled.AspNetRolesId = Int32.Parse(id);


            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            else
            {
                role.Name = model.RoleName;
                roleEnabled.IsEnabled = model.IsEnabled;
                roleEnabled.AspNetRolesId = Int32.Parse(model.RoleId);

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            };

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
            var data_ =  model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType));
               
              foreach(var data in data_) 
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
