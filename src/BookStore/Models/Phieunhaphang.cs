using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class PhieuNhapHang
    {
        public PhieuNhapHang()
        {
            ChiTietPhieuNhapHang = new HashSet<ChiTietPhieuNhapHang>();
            PhieuChi = new HashSet<PhieuChi>();
            PhieuNhanHang = new HashSet<PhieuNhanHang>();
            PhieuTraNhapHang = new HashSet<PhieuTraNhapHang>();
        }

        [Display(Name = "Id", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int Id { get; set; }

        [Display(Name = "NgayLap", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime NgayLap { get; set; }

        [Display(Name = "TongTien",
            ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; }

        [Display(Name = "NhanVien",
            ResourceType = typeof(Resources.DataAnnotations))]
        public int NhanVienId { get; set; }

        [Display(Name = "TrangThai", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int TrangThaiId { get; set; }

        [Display(Name = "NhaCungCap", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int NhaCungCapId { get; set; }

        public string GhiChu { get; set; }

        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual ICollection<PhieuChi> PhieuChi { get; set; }
        public virtual ICollection<PhieuNhanHang> PhieuNhanHang { get; set; }
        public virtual ICollection<PhieuTraNhapHang> PhieuTraNhapHang { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual Staff NhanVien { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}