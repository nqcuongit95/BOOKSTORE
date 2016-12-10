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
using BookStore.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    
    public class AdminController : Controller
    {
        private RoleManager<Role> _roleManager;
        private SignInManager<Staff> _signInManager;
        private UserManager<Staff> _userManager;
        private IBookStoreData _bookStoreData;

        public AdminController(UserManager<Staff> userManager,
                               SignInManager<Staff> signInManager,
                               RoleManager<Role> roleManager,
                               IBookStoreData bookStoreData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _bookStoreData = bookStoreData;
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

        public IActionResult NotificationMessage(Notification model)
        {
            return PartialView("_Message",model);
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
                        var noti = new Notification
                        {
                            Title = "Thành Công",
                            Content = "Thêm quyền thành công",
                            Button = "Hoàn tất"
                        };

                        status.Url = Url.Action("NotificationMessage", "Admin",noti);

                        return Json(status);
                    }
                }
                else
                {
                    status.Type = Result.Error;
                    status.Details = "Quyền đã có trước đó.";
                    return Json(status);
                }
            }

            status.Type = Result.Error;
            return Json(status);
        }

        public async Task<IActionResult> CreateUser()
        {
            var roles = await _bookStoreData.GetListRoles();

            var model = new RegisterUserViewModel();
            model.Roles = roles.Select(r => r.Name).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterUserViewModel model)
        {
            Notification msg;

            if (ModelState.IsValid)
            {
                var user = new Staff { UserName = model.Username };

                //other fields
                user.FullName = model.FullName;
                user.DateCreate = System.DateTime.Now;

                var role = await _roleManager.FindByNameAsync(model.AssignedRole);

                //if role is existing
                if (await _roleManager.RoleExistsAsync(role.Name))
                {
                    var createResult = await _userManager.CreateAsync(user, model.Password);
                    //if create was succeeded
                    if (createResult.Succeeded)
                    {
                        var finalResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (finalResult.Succeeded)
                        {
                            msg = new Notification
                            {
                                Title = "Thành Công",
                                Content = "Thêm mới user thành công",
                                Button = "Hoàn tất"
                            };

                            return PartialView("_Message", msg);
                        }
                    }
                    else
                    {
                        foreach (var error in createResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }                
            }

            return View(model);
        }

        public async Task<IActionResult> ViewRole(int id)
        {
            var role =await  _roleManager.FindByIdAsync(id.ToString());
            
            return PartialView("_ViewRole",role);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return PartialView("_DeleteRole",role);
        }

        public async Task<IActionResult> ConfirmDeleteRole(int id)
        {
            var status = new Status();
            Notification model;
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                model = new Notification
                {
                    Title = "Thành Công",
                    Content = "Đã xóa quyền " + role.Name,
                    Button = "Hoàn Tất"
                };

                return PartialView("_Message", model);

            }

            model = new Notification
            {
                Title = "Xảy Ra Lỗi",                
                Button = "Hoàn Tất"
            };

            foreach (var error in result.Errors)
            {
                model.Content += error + "/n";
            }            

            return PartialView("_Message", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var model = new RoleViewModel
            {
                Name = role.Name,
                Description = role.Description
            };

            return PartialView("_EditRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(int id, RoleViewModel model)
        {
            Notification msg;

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());

                role.Name = model.Name;
                role.Description = model.Description;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    msg = new Notification
                    {
                        Title = "Thành Công",
                        Content = "Cập nhật quyền thành công",
                        Button = "Hoàn tất"
                    };

                    return PartialView("_Message", msg);
                }
            }

            msg = new Notification
            {
                Title = "Lỗi",
                Content = "Có lỗi đã xảy ra, vui lòng thử lại.",
                Button = "Quay lại"
            };

            return PartialView("_Message", msg);
            
        }
    }
}
