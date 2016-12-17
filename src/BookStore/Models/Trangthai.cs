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
        }

        public int Id { get; set; }
        public string TenTrangThai { get; set; }
        public string VietTat { get; set; }

        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<HangHoa> HangHoa { get; set; }
    }
}
