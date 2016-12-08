using BookStore.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuChi
    {
        public int Id { get; set; }
        public int NhanVienId { get; set; }
        public DateTime NgayLap { get; set; }
        public int? PhieuTraHangId { get; set; }
        public int? PhieuNhapHangId { get; set; }
        public decimal TongTien { get; set; }

        public virtual User NhanVien { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
        public virtual PhieuTraHang PhieuTraHang { get; set; }
    }
}
