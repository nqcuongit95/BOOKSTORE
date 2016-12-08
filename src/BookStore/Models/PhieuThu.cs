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
        public string NhanVienId { get; set; }
        public decimal TongTien { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual User NhanVien { get; set; }
        public virtual PhieuTraNhapHang PhieuTraNhapHang { get; set; }
    }
}
