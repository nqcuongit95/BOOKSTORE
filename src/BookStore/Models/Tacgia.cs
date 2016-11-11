using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Tacgia
    {
        public Tacgia()
        {
            Sach = new HashSet<Sach>();
        }

        public int MaTacGia { get; set; }
        public string TenTacGia { get; set; }
        public int MaQuocGia { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
        public virtual Quocgia MaQuocGiaNavigation { get; set; }
    }
}
