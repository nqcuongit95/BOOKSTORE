using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class LoaiKhachHang
    {
        public LoaiKhachHang()
        {
            KhachHang = new HashSet<KhachHang>();
        }

        public int Id { get; set; }
        public string TenLoaiKhachHang { get; set; }

        public virtual ICollection<KhachHang> KhachHang { get; set; }
    }
}
