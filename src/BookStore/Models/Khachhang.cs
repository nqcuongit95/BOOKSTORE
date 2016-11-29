﻿using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHang = new HashSet<DonHang>();
            PhieuTraHang = new HashSet<PhieuTraHang>();
        }

        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public int LoaiKhachHangId { get; set; }
        public DateTime NgayLap { get; set; }

        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<PhieuTraHang> PhieuTraHang { get; set; }
        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
    }
}
