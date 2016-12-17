using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class LoaiPhieu
    {
        public LoaiPhieu()
        {
            PhieuChi = new HashSet<PhieuChi>();
            PhieuThu = new HashSet<PhieuThu>();
        }

        public int Id { get; set; }
        public string TenLoaiPhieu { get; set; }
        public string Loai { get; set; }

        public virtual ICollection<PhieuChi> PhieuChi { get; set; }
        public virtual ICollection<PhieuThu> PhieuThu { get; set; }
    }
}
