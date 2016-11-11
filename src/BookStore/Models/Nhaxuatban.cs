using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Nhaxuatban
    {
        public Nhaxuatban()
        {
            Sach = new HashSet<Sach>();
        }

        public int MaNxb { get; set; }
        public string TenNxb { get; set; }
        public int MaQuocGia { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
        public virtual Quocgia MaQuocGiaNavigation { get; set; }
    }
}
