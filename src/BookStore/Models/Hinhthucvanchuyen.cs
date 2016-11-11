using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Hinhthucvanchuyen
    {
        public Hinhthucvanchuyen()
        {
            Donhang = new HashSet<Donhang>();
        }

        public int MaVanChuyen { get; set; }
        public string HinhThucVanChuyen { get; set; }

        public virtual ICollection<Donhang> Donhang { get; set; }
    }
}
