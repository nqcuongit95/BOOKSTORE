using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Phieutrahang
    {
        public Phieutrahang()
        {
            Chitietphieutra = new HashSet<Chitietphieutra>();
        }

        public int MaPhieuTra { get; set; }
        public DateTime NgayTra { get; set; }
        public int MaKhachHang { get; set; }
        public string GhiChu { get; set; }
        public int MaDonHang { get; set; }

        public virtual ICollection<Chitietphieutra> Chitietphieutra { get; set; }
        public virtual Donhang MaDonHangNavigation { get; set; }
        public virtual Khachhang MaKhachHangNavigation { get; set; }
    }
}
