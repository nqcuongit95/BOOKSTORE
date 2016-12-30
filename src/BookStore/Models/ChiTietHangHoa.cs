using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class ChiTietHangHoa
    {
        public int Id { get; set; }
        public int HangHoaId { get; set; }

        [Display(Name = "ThuocTinh",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string ThuocTinh { get; set; }

        [Display(Name = "GiaTri",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(100, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string GiaTri { get; set; }

        public virtual HangHoa HangHoa { get; set; }
    }
}