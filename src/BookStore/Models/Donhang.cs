using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Donhang
    {
        public Donhang()
        {
            Chitietdonhang = new HashSet<Chitietdonhang>();
            Phieutrahang = new HashSet<Phieutrahang>();
        }

        public int MaDonHang { get; set; }
        public DateTime NgayLap { get; set; }
        public int MaKhachHang { get; set; }
        public int MaTrangThai { get; set; }
        public int MaVanChuyen { get; set; }
        public DateTime? NgayGiaoHang { get; set; }
        public decimal TongTien { get; set; }

        public virtual ICollection<Chitietdonhang> Chitietdonhang { get; set; }
        public virtual ICollection<Phieutrahang> Phieutrahang { get; set; }
        public virtual Khachhang MaKhachHangNavigation { get; set; }
        public virtual Trangthai MaTrangThaiNavigation { get; set; }
        public virtual Hinhthucvanchuyen MaVanChuyenNavigation { get; set; }
    }
}
