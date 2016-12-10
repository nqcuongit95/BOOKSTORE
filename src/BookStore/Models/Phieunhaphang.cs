using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class PhieuNhapHang
    {
        public PhieuNhapHang()
        {
            ChiTietPhieuNhapHang = new HashSet<ChiTietPhieuNhapHang>();
        }

        public int Id { get; set; }

        [Display(Name = "NgayLap", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime NgayLap { get; set; }

        [Display(Name = "TongTien", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
    }
}
