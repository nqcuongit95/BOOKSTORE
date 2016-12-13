using BookStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewComponents.Account
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        private UserManager<Staff> _userManager;
        public LoginLogoutViewComponent(UserManager<Staff> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Staff staff;
            
            if (User.Identity.IsAuthenticated)
            {
                staff = await GetCurrentUserAsync();
                return View("Default", staff.FullName);
            }

            return View("Default","");

        }

        private Task<Staff> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
