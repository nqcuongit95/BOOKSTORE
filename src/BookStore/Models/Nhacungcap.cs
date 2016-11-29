using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            ChiTietPhieuNhapHang = new HashSet<ChiTietPhieuNhapHang>();
            HangHoa = new HashSet<HangHoa>();
        }

        public int Id { get; set; }
        public string TenNhaCungCap { get; set; }
        public DateTime NgayLap { get; set; }

        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual ICollection<HangHoa> HangHoa { get; set; }
    }
}
