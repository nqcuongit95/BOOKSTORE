using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class RegisterUserViewModel
    {   
        [Required]
        [Display(Name = "Họ Tên")]
        [StringLength(60, ErrorMessage = "{0} Không được quá {1} kí tự.")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Tài Khoản")]
        [StringLength(30,ErrorMessage ="{0} phải ít nhất là {2} kí tự.",MinimumLength =5)]
        public string Username { get; set; }
        
        [Required,DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0} phải ít nhất là {2} kí tự.", MinimumLength = 6)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Mật Khẩu")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Cấp quyền cho tài khoản.")]        
        public string AssignedRole { get; set; }
        public List<string> Roles { get; set; }

    }
}
