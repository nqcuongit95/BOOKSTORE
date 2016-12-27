using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class PhieuTraHang
    {
        public PhieuTraHang()
        {
            ChiTietPhieuTraHang = new HashSet<ChiTietPhieuTraHang>();
            PhieuChi = new HashSet<PhieuChi>();
        }

        [Display(Name = "Id", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int Id { get; set; }

        [Display(Name = "NgayLap", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime NgayLap { get; set; }

        [Display(Name = "KhachHang", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int KhachHangId { get; set; }

        [Display(Name = "DonHang", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int DonHangId { get; set; }

        [Display(Name = "GhiChu", ResourceType = typeof(
            Resources.DataAnnotations))]
        [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự.")]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public string GhiChu { get; set; }

        [Display(Name = "TongTien",
            ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; }

        [Display(Name = "NhanVien",
            ResourceType = typeof(Resources.DataAnnotations))]
        public int NhanVienId { get; set; }

        public virtual ICollection<ChiTietPhieuTraHang> ChiTietPhieuTraHang { get; set; }
        public virtual ICollection<PhieuChi> PhieuChi { get; set; }
        public virtual DonHang DonHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual Staff NhanVien { get; set; }
    }
}
