using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Theloaisach
    {
        public Theloaisach()
        {
            Sach = new HashSet<Sach>();
        }

        public int MaLoaiSach { get; set; }
        public string TenLoaiSach { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
    }
}
