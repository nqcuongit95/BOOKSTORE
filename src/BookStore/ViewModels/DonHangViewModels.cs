using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class DonHangViewModels
    {
        //public DonHang donHang { get; set; }
        public KhachHang khachhang { get; set; }
        public TrangThai trangthai { get; set; }
        public DonHang donhang { get; set; }

        public String MaDonHang { get; set; }
        public String TenKhachHang { get; set; }
        public int KhachHangID { get; set; }
        public DateTime NgayLap { get; set; }
        public int TrangThaiID { get; set; }
        public String TenTrangThai { get; set; }
        public decimal TongTien { get; set; }

    }
}
