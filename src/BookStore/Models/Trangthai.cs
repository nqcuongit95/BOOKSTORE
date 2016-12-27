using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            DonHang = new HashSet<DonHang>();
            HangHoa = new HashSet<HangHoa>();
            PhieuNhapHang = new HashSet<PhieuNhapHang>();
        }

        public int Id { get; set; }
        public string TenTrangThai { get; set; }
        public string VietTat { get; set; }
        public string Loai { get; set; }

        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<HangHoa> HangHoa { get; set; }
        public virtual ICollection<PhieuNhapHang> PhieuNhapHang { get; set; }
    }
}
