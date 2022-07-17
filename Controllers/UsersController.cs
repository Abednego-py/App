using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> _roleManager)
        {
            this.userManager = userManager;
            this._roleManager = _roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var listusers = await userManager.Users.ToListAsync();
            return View(listusers);
        }




        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsEnabled = user.IsEnabled,
                Roles = userRoles
                
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;   
                user.IsEnabled = model.IsEnabled;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }



        //public async Task<IActionResult> ManageUser()
        //{
        //    var users = await userManager.Users.ToListAsync();
        //    var userRolesViewModel = new List<UserRolesViewModel>();
        //    foreach (ApplicationUser user in users)
        //    {
        //        var thisViewModel = new UserRolesViewModel();
        //        thisViewModel.UserId = user.Id;
        //        thisViewModel.Email = user.Email;

        //        thisViewModel.Roles = 

        //        userRolesViewModel.Add(thisViewModel);
        //    }
        //    return View(userRolesViewModel);
        //}

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var allRoles = await _roleManager.Roles.ToListAsync();

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }


            var roles = await userManager.GetRolesAsync(user);

            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            
            //foreach (var role in allRoles)
            //{
            //    if(role.IsEnabled == true)
            //    {
                    result = await userManager.AddToRolesAsync(user,
                        model.Where(x => x.IsSelected).Select(y => y.RoleName));
            //    }
            //    ModelState.AddModelError("", "Cannot add role to user");
            //    return View(model);

            //}

            //result = await userManager.AddToRolesAsync(user,
            //    model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _userManager.)
        //    {
        //        return NotFound();
        //    }

        //    var claims = await _context.Claims
        //        .FirstOrDefaultAsync(m => m.ClaimsId == id);
        //    if (claims == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(claims);
        //}
    }


}
