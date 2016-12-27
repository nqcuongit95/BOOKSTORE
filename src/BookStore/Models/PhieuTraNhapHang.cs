using BookStore.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuTraNhapHang
    {
        public PhieuTraNhapHang()
        {
            ChiTietPhieuTraNhapHang = new HashSet<ChiTietPhieuTraNhapHang>();
            PhieuThu = new HashSet<PhieuThu>();
        }

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public int NhanVienId { get; set; }
        public int PhieuNhapHangId { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<ChiTietPhieuTraNhapHang> ChiTietPhieuTraNhapHang { get; set; }
        public virtual ICollection<PhieuThu> PhieuThu { get; set; }
        public virtual Staff NhanVien { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}
