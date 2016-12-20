using BookStore.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuNhanHang
    {
        public int Id { get; set; }
        public int PhieuNhapHangId { get; set; }
        public DateTime NgayLap { get; set; }
        public int NhanVienId { get; set; }
        public decimal TongTien { get; set; }

        public virtual Staff NhanVien { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}
