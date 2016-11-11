using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Nhacungcap
    {
        public Nhacungcap()
        {
            Phieunhaphang = new HashSet<Phieunhaphang>();
        }

        public int MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Phieunhaphang> Phieunhaphang { get; set; }
    }
}
