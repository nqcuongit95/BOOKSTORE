using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Phieunhaphang
    {
        public Phieunhaphang()
        {
            Chitietphieunhap = new HashSet<Chitietphieunhap>();
        }

        public int MaPhieuNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public int MaNcc { get; set; }

        public virtual ICollection<Chitietphieunhap> Chitietphieunhap { get; set; }
        public virtual Nhacungcap MaNccNavigation { get; set; }
    }
}
