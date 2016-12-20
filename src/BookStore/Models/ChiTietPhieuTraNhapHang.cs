using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietPhieuTraNhapHang
    {
        public int Id { get; set; }
        public int PhieuTraNhapHangId { get; set; }
        public int ChiTietPhieuNhapHangId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTra { get; set; }

        public virtual ChiTietPhieuNhapHang ChiTietPhieuNhapHang { get; set; }
        public virtual PhieuTraNhapHang PhieuTraNhapHang { get; set; }
    }
}
