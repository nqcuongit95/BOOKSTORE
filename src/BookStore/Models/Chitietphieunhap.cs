using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Chitietphieunhap
    {
        public int MaCtpn { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }

        public virtual Hanghoa MaHangHoaNavigation { get; set; }
        public virtual Phieunhaphang MaPhieuNhapNavigation { get; set; }
    }
}
