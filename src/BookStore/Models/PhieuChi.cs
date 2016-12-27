using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class PhieuChi
    {
        [Display(Name = "Id", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int Id { get; set; }

        [Display(Name = "NhanVien", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int NhanVienId { get; set; }

        [Display(Name = "NgayLap", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime NgayLap { get; set; }

        [Display(Name = "PhieuTraHang", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int? PhieuTraHangId { get; set; }

        [Display(Name = "PhieuNhapHang", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int? PhieuNhapHangId { get; set; }

        [Display(Name = "TongTien",
            ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; }

        [Display(Name = "LoaiPhieu", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int LoaiPhieuId { get; set; }

        public virtual LoaiPhieu LoaiPhieu { get; set; }
        public virtual Staff NhanVien { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
        public virtual PhieuTraHang PhieuTraHang { get; set; }
    }
}
