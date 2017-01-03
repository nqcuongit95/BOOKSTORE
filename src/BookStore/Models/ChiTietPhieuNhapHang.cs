using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class ChiTietPhieuNhapHang
    {
        public ChiTietPhieuNhapHang()
        {
            ChiTietPhieuTraNhapHang = new HashSet<ChiTietPhieuTraNhapHang>();
        }

        public int Id { get; set; }
        public int PhieuNhapHangId { get; set; }
        public int HangHoaId { get; set; }

        [Display(Name = "SoLuong",
            ResourceType = typeof(Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public int SoLuong { get; set; }

        [Display(Name = "GiaNhap", ResourceType = typeof(
            Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public decimal GiaNhap { get; set; }

        public virtual ICollection<ChiTietPhieuTraNhapHang> ChiTietPhieuTraNhapHang { get; set; }
        public virtual HangHoa HangHoa { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}