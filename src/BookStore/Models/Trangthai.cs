using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            DonHang = new HashSet<DonHang>();
            HangHoa = new HashSet<HangHoa>();
            PhieuNhapHang = new HashSet<PhieuNhapHang>();
        }

        public int Id { get; set; }

        [Display(Name = "TenTrangThai",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(100, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string TenTrangThai { get; set; }

        [Display(Name = "Loai",
            ResourceType = typeof(Resources.DataAnnotations))]
        [MaxLength(20, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string Loai { get; set; }

        [Display(Name = "VietTat",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(20, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string VietTat { get; set; }
        public string Loai { get; set; }

        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<HangHoa> HangHoa { get; set; }
        public virtual ICollection<PhieuNhapHang> PhieuNhapHang { get; set; }
    }
}
