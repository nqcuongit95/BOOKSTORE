using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietPhieuTraHang
    {
        public int Id { get; set; }
        public int PhieuTraHangId { get; set; }
        public int ChiTietDonHangId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTra { get; set; }

        public virtual PhieuTraHang PhieuTraHang { get; set; }
    }
}
