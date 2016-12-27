using BookStore.Entities;
using System;
using System.Collections.Generic;

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

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public int NhanVienId { get; set; }
        public int TrangThaiId { get; set; }
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
