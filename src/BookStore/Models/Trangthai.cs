using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Trangthai
    {
        public Trangthai()
        {
            Donhang = new HashSet<Donhang>();
            Hanghoa = new HashSet<Hanghoa>();
        }

        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }

        public virtual ICollection<Donhang> Donhang { get; set; }
        public virtual ICollection<Hanghoa> Hanghoa { get; set; }
    }
}
