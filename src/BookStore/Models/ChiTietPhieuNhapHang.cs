using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietPhieuNhapHang
    {
        public ChiTietPhieuNhapHang()
        {
            ChiTietPhieuTraNhapHang = new HashSet<ChiTietPhieuTraNhapHang>();
        }

        public int Id { get; set; }
        public int PhieuNhapHangId { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }

        public virtual ICollection<ChiTietPhieuTraNhapHang> ChiTietPhieuTraNhapHang { get; set; }
        public virtual HangHoa HangHoa { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}
