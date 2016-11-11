using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Nhasanxuat
    {
        public Nhasanxuat()
        {
            Vanphongpham = new HashSet<Vanphongpham>();
        }

        public int MaNsx { get; set; }
        public string TenNsx { get; set; }
        public int? MaQuocGia { get; set; }

        public virtual ICollection<Vanphongpham> Vanphongpham { get; set; }
        public virtual Quocgia MaQuocGiaNavigation { get; set; }
    }
}
