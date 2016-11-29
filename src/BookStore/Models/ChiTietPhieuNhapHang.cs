using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietPhieuNhapHang
    {
        public int Id { get; set; }
        public int PhieuNhapHangId { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public int NhaCungCapId { get; set; }

        public virtual HangHoa HangHoa { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}
