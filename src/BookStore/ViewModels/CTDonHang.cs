using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class CTDonHang
    {
        public int ID { get; set; }
        public int DonHangId { get; set; }
        public int HangHoaId { get; set; }
        public string TenHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
