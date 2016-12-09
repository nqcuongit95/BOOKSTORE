using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookStore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BookStore.ViewModels;
using BookStore.Helper;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    
    public class AdminController : Controller
    {
        private RoleManager<Role> _roleManager;
        private SignInManager<Staff> _signInManager;
        private UserManager<Staff> _userManager;

        public AdminController(UserManager<Staff> userManager,
                               SignInManager<Staff> signInManager,
                               RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Roles()
        {
            var model = _roleManager.Roles.ToList();

            return View(model);
        }

        public IActionResult SucceedMessage()
        {
            return PartialView("_SucceedMessage");
        }
        
        public IActionResult CreateRole()
        {
            return PartialView("_CreateRoleModal");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            Status status = new Status();

            if (ModelState.IsValid)
            {
                if (!await _roleManager.RoleExistsAsync(model.Name))
                {
                    Role newRole = new Role();
                    newRole.Name = model.Name;
                    newRole.Description = model.Description;

                    var result = await _roleManager.CreateAsync(newRole);

                    if (result.Succeeded)
                    {
                        status.Type = Result.Succeed;
                        status.Url = Url.Action("SucceedMessage","Admin");

                        return Json(status);
                    }
                }
            }

            status.Type = Result.Error;
            return Json(status);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(RegisterUserViewModel model)
        {
            
            return View();
        }

    }
}
