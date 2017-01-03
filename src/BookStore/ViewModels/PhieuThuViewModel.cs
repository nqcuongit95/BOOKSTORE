using System;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class PhieuThuViewModel
    {
        public int ID { get; set; }
        public DateTime NgayLap { get; set; }
        public int? DonHangId { get; set; }
        public int? PhieuNhapHangId { get; set; }
        public decimal TongTien { get; set; }
        public int LoaiPhieuId { get; set; }
        public int NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public int? KhachHangId { get; set; }
        public int? NCCId { get; set; }


        public string TenLoaiPhieu { get; set; }
        public string DoiTuong { get; set; }
        public SelectList LoaiPhieu { get; set; }
        public SelectList ListDoiTuong { get; set; }
        public string TenKhachHang { get; set; }
        public string TenNhaCungCap { get; set; }
    }
}
