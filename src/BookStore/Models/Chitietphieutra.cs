using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Chitietphieutra
    {
        public int MaCtpt { get; set; }
        public int MaPhieuTra { get; set; }
        public int MaCtdh { get; set; }
        public int SoLuong { get; set; }

        public virtual Chitietdonhang MaCtdhNavigation { get; set; }
        public virtual Phieutrahang MaPhieuTraNavigation { get; set; }
    }
}
