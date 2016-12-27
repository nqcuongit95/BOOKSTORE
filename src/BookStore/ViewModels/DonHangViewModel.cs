using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class DonHangViewModel
    {
        public int ID { get; set; }
        public DateTime NgayLap { get; set; }
        public int KhachHangId { get; set; }
        public string TenKhachHang { get; set; }
        public int TrangThaiId { get; set; }
        public string TenTrangThai { get; set; }
        public decimal TongTien { get; set; }
        public int NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public List<CTDonHang> details;
        public CustomerInfoViewModel khachhang;
    }
}
