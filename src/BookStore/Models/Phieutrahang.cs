using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuTraHang
    {
        public PhieuTraHang()
        {
            ChiTietPhieuTraHang = new HashSet<ChiTietPhieuTraHang>();
        }

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }
        public int KhachHangId { get; set; }
        public int DonHangId { get; set; }
        public string GhiChu { get; set; }
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietPhieuTraHang> ChiTietPhieuTraHang { get; set; }
        public virtual DonHang DonHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
