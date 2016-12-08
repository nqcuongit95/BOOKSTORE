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
            PhieuTraNhapHang = new HashSet<PhieuTraNhapHang>();
        }

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual ICollection<PhieuChi> PhieuChi { get; set; }
        public virtual ICollection<PhieuTraNhapHang> PhieuTraNhapHang { get; set; }
    }
}
