using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Donhang = new HashSet<Donhang>();
            Phieutrahang = new HashSet<Phieutrahang>();
        }

        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Donhang> Donhang { get; set; }
        public virtual ICollection<Phieutrahang> Phieutrahang { get; set; }
    }
}
