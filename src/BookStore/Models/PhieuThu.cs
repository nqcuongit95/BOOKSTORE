using BookStore.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuThu
    {
        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public int? DonHangId { get; set; }
        public int? PhieuTraNhapHangId { get; set; }
        public int NhanVienId { get; set; }
        public decimal TongTien { get; set; }
        public int LoaiPhieuId { get; set; }
        public int? KhachHangId { get; set; }
        public string GhiChu { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual LoaiPhieu LoaiPhieu { get; set; }
        public virtual Staff NhanVien { get; set; }
        public virtual PhieuTraNhapHang PhieuTraNhapHang { get; set; }
    }
}
