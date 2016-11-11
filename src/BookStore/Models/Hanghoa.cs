using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Hanghoa
    {
        public Hanghoa()
        {
            Chitietdonhang = new HashSet<Chitietdonhang>();
            Chitietphieunhap = new HashSet<Chitietphieunhap>();
        }

        public int MaHangHoa { get; set; }
        public int? DaBan { get; set; }
        public int? TonKho { get; set; }
        public decimal? GiaBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public int MaTrangThai { get; set; }

        public virtual ICollection<Chitietdonhang> Chitietdonhang { get; set; }
        public virtual ICollection<Chitietphieunhap> Chitietphieunhap { get; set; }
        public virtual Sach MaHangHoaNavigation { get; set; }
        public virtual Vanphongpham MaHangHoa1 { get; set; }
        public virtual Trangthai MaTrangThaiNavigation { get; set; }
    }
}
