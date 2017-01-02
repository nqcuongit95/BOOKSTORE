using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
namespace BookStore.ViewModels
{
    public class TraHangDetailViewModel
    {
        public int ID { get; set; }
        public int TraHangId { get; set; }
        public int DonHangId { get; set; }
        public int HangHoaId { get; set; }
        public string TenHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTra { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
