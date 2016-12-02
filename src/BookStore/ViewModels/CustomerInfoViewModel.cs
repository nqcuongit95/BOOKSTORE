using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class CustomerInfoViewModel
    {
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }        
        public string Email { get; set; }
        public string TenLoaiKhachHang { get; set; }
        public DateTime NgayLap { get; set; }
    }
}
