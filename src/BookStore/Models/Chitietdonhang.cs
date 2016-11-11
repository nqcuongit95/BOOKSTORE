using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Chitietdonhang
    {
        public Chitietdonhang()
        {
            Chitietphieutra = new HashSet<Chitietphieutra>();
        }

        public int MaCtdh { get; set; }
        public int MaDonHang { get; set; }
        public int SoLuong { get; set; }
        public int MaHangHoa { get; set; }
        public decimal GiaBan { get; set; }

        public virtual ICollection<Chitietphieutra> Chitietphieutra { get; set; }
        public virtual Donhang MaDonHangNavigation { get; set; }
        public virtual Hanghoa MaHangHoaNavigation { get; set; }
    }
}
