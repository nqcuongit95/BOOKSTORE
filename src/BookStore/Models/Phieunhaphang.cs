using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuNhapHang
    {
        public PhieuNhapHang()
        {
            ChiTietPhieuNhapHang = new HashSet<ChiTietPhieuNhapHang>();
        }

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
    }
}
