using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class PhieuThuViewModel
    {
        public int ID { get; set; }
        public DateTime NgayLap { get; set; }
        public int DonHangId { get; set; }
        public int? PhieuNhapHangId { get; set; }
        public decimal TongTien { get; set; }
        public int LoaiPhieuId { get; set; }
        public string TenLoaiPhieu { get; set; }
        public int NhanVienId { get; set; }
        public int KhachHangId { get; set; }
        public string TenKhachHang { get; set; }
    }
}
