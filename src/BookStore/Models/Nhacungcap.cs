using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoa = new HashSet<HangHoa>();
            PhieuNhapHang = new HashSet<PhieuNhapHang>();
        }

        public int Id { get; set; }
        public string TenNhaCungCap { get; set; }
        public DateTime NgayLap { get; set; }

        public virtual ICollection<HangHoa> HangHoa { get; set; }
        public virtual ICollection<PhieuNhapHang> PhieuNhapHang { get; set; }
    }
}
