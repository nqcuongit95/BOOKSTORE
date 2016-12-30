using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class NhanHieu
    {
        public NhanHieu()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        [Display(Name = "Id", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int Id { get; set; }

        [Display(Name = "TenNhanHieu",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string TenNhanHieu { get; set; }

        public virtual ICollection<HangHoa> HangHoa { get; set; }
    }
}