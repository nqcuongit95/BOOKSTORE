using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PhieuKiemKho
    {
        public PhieuKiemKho()
        {
            ChiTietPhieuKiemKho = new HashSet<ChiTietPhieuKiemKho>();
        }

        public int Id { get; set; }
        public DateTime NgayLap { get; set; }

        public virtual ICollection<ChiTietPhieuKiemKho> ChiTietPhieuKiemKho { get; set; }
    }
}
