using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Nhớ Tài Khoản")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
