using BookStore.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDonHang = new HashSet<ChiTietDonHang>();
            PhieuThu = new HashSet<PhieuThu>();
            PhieuTraHang = new HashSet<PhieuTraHang>();
        }

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public int KhachHangId { get; set; }
        public int TrangThaiId { get; set; }
        public decimal TongTien { get; set; }
        public int NhanVienId { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual ICollection<PhieuThu> PhieuThu { get; set; }
        public virtual ICollection<PhieuTraHang> PhieuTraHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual User NhanVien { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}
