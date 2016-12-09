using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Tên Quyền")]
        public string Name { get; set; }

        [Display(Name ="Mô Tả")]
        public string Description { get; set; }

    }
}
