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

        public String MaDonHang { get; set; }
        public String KhachHang { get; set; }
        public int KhachHangID { get; set; }
        public DateTime NgayLap { get; set; }
        public int TrangThaiID { get; set; }
        public String TrangThai { get; set; }
        public decimal TongTien { get; set; }

    }
}
