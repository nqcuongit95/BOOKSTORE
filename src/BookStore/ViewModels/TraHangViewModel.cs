using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class TraHangViewModel
    {
        public int ID { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public int NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public int KhachHangId { get; set; }
        public int DonHangId { get; set; }
        public string TenKhachHang { get; set; }
        public SelectList ListDonHang { get; set; }
        public string GhiChu { get; set; }
        public List<TraHangDetailViewModel> details { get; set; }
    }
}
