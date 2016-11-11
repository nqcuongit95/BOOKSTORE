using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Quocgia
    {
        public Quocgia()
        {
            Nhasanxuat = new HashSet<Nhasanxuat>();
            Nhaxuatban = new HashSet<Nhaxuatban>();
            Tacgia = new HashSet<Tacgia>();
        }

        public int MaQuocGia { get; set; }
        public string TenQuocGia { get; set; }

        public virtual ICollection<Nhasanxuat> Nhasanxuat { get; set; }
        public virtual ICollection<Nhaxuatban> Nhaxuatban { get; set; }
        public virtual ICollection<Tacgia> Tacgia { get; set; }
    }
}
