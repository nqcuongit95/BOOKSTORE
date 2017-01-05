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
using Microsoft.AspNetCore.Authorization;
using BookStore.ViewModels.Admin;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    //[Authorize(Roles="Admin")]
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

        public async Task<IActionResult> ListUser(string sortOrder, string searchString,
                                               string currentFilter, int? page,
                                               int? firstShowedPage, int? lastShowedPage)
        {

            ViewData["QueryName"] = nameof(searchString);
            ViewData["SortDirection"] = "up";

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var staffs = _bookStoreData.GetListStaffQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                staffs = staffs.Where(c => c.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    staffs = staffs.OrderByDescending(c => c.UserName);
                    ViewData["SortDirection"] = "down";
                    break;
                case "Date":
                    staffs = staffs.OrderBy(c => c.DateCreate);
                    break;
                case "date_desc":
                    staffs = staffs.OrderByDescending(c => c.DateCreate);
                    break;
                default:
                    staffs = staffs.OrderBy(c => c.UserName);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 5;
            int numberOfDisplayPages = 5;

            return PartialView("_ListUser", await PaginatedList<Staff>.
                        CreateAsync(staffs, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
        }

        public IActionResult Roles()
        {
            var model = _roleManager.Roles.ToList();

            return View(model);
        }

        public IActionResult NotificationMessage(Notification model)
        {
            return PartialView("_Message", model);
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

                        status.Url = Url.Action("NotificationMessage", "Admin", noti);

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
            var role = await _roleManager.FindByIdAsync(id.ToString());
            
            return PartialView("_ViewRole", role);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return PartialView("_DeleteRole", role);
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

        //for user
        public async Task<IActionResult> ViewUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var listRole = await _userManager.GetRolesAsync(user);

            var model = new StaffViewModel
            {
                ID = user.Id,
                FullName = user.FullName,
                DateCreate = user.DateCreate.GetValueOrDefault(),
                Phone = user.PhoneNumber,
                UserName = user.UserName,
                Role = listRole.FirstOrDefault()
            };
            return PartialView("_ViewUser", model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _bookStoreData.GetListRoles();
            
            var listRole = roles.Select(r => r.Name).ToList();
            var listUserRole = await _userManager.GetRolesAsync(user);
            var userRole = listUserRole.FirstOrDefault();

            var model = new EditUserViewModel
            {
                ID = id,
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                AssignedRole = userRole,
                Roles = listRole,
                Username = user.UserName
            };

            return PartialView("_EditUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            Notification noti;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.ID.ToString());

                //reset password if password data is not null
                if (model.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                    if (!result.Succeeded)
                    {
                        noti = new Notification
                        {
                            Title = "Thất bại",
                            Content = "Cập nhật thất bại, vui lòng thử lại.",
                            Icon = "remove",
                            MessageType = "negative",
                            Button = "Quay lại"
                        };
                        return PartialView("_Notify", noti);
                    }
                }

                //update user infomation
                user.FullName = model.FullName;
                user.PhoneNumber = model.Phone;
                user.UserName = model.Username;

                //update role if it has changed
                string oldRole = "";
                var listUserRoles = await _userManager.GetRolesAsync(user);
                if (model.AssignedRole != null)
                {
                    if (listUserRoles.Count > 0)
                    {
                        oldRole = listUserRoles.First();
                        
                        if (model.AssignedRole != oldRole)
                        {
                            var removeResult = await _userManager.RemoveFromRoleAsync(user, oldRole);
                            var newRoleResult = await _userManager.AddToRoleAsync(user, model.AssignedRole);
                            if (!removeResult.Succeeded || !newRoleResult.Succeeded)
                            {
                                noti = new Notification
                                {
                                    Title = "Thất bại",
                                    Content = "Có lỗi xảy ra, vui lòng thử lại.",
                                    Icon = "remove",
                                    MessageType = "negative",
                                    Button = "Quay lại"
                                };
                                return PartialView("_Notify", noti);
                            }
                        }
                    }
                    else
                    {

                        var newRoleResult = await _userManager.AddToRoleAsync(user, model.AssignedRole);
                        if (!newRoleResult.Succeeded)
                        {
                            noti = new Notification
                            {
                                Title = "Thất bại",
                                Content = "Có lỗi xảy ra, vui lòng thử lại.",
                                Icon = "remove",
                                MessageType = "negative",
                                Button = "Quay lại"
                            };
                            return PartialView("_Notify", noti);
                        }
                    }
                }
                //finally, update user
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    noti = new Notification
                    {
                        Title = "Thành công",
                        Content = "Cập nhật thông tin nhân viên thành công.",
                        Icon = "checkmark",
                        MessageType = "positive",
                        Button = "Hoàn tất"
                    };
                    return PartialView("_Notify", noti);
                }
            }

            noti = new Notification
            {
                Title = "Thất bại",
                Content = "Có lỗi xảy ra, vui lòng thử lại.",
                Icon = "remove",
                MessageType = "negative",
                Button = "Quay lại"
            };
            return PartialView("_Notify", noti);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return PartialView("_DeleteUser", user);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteUser(int id)
        {
            try
            {

                Notification noti;
                var user = await _userManager.FindByIdAsync(id.ToString());
                var fullName = user.FullName;
                if (user.UserName == "admin")
                {
                    noti = new Notification
                    {
                        Title = "Thất bại",
                        Content = "Không được xóa account này !",
                        Icon = "remove",
                        MessageType = "negative",
                        Button = "Quay lại"
                    };
                    return PartialView("_Notify", noti);
                }
                else
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    if (userRoles.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, userRoles);
                    }

                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        _bookStoreData.Commit();
                        noti = new Notification
                        {
                            Title = "Thành công",
                            Content = "Đã xóa nhân viên " + fullName + " !",
                            Icon = "checkmark",
                            MessageType = "positive",
                            Button = "Hoàn tất"
                        };
                        return PartialView("_Notify", noti);
                    }
                }

                noti = new Notification
                {
                    Title = "Thất bại",
                    Content = "Không được xóa account này !",
                    Icon = "remove",
                    MessageType = "negative",
                    Button = "Quay lại"
                };
                return PartialView("_Notify", noti);
            }
            catch (Exception)
            {

                var noti = new Notification
                {
                    Title = "Thất bại",
                    Content = "Không được xóa nhân viên này !",
                    Icon = "remove",
                    MessageType = "negative",
                    Button = "Quay lại"
                };
                return PartialView("_Notify", noti);
            }
        }

    }
}
