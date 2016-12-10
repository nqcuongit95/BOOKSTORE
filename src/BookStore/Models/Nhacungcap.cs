using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [DisplayName("Nhà cung cấp"),]
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            ChiTietPhieuNhapHang = new HashSet<ChiTietPhieuNhapHang>();
            HangHoa = new HashSet<HangHoa>();
        }

        public int Id { get; set; }

        [Display(Name = "TenNhaCungCap",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string TenNhaCungCap { get; set; }

        [Display(Name = "NgayLap",
            ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime NgayLap { get; set; }

        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual ICollection<HangHoa> HangHoa { get; set; }
    }
}
