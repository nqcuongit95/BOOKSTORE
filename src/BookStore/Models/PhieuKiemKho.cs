using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class PhieuKiemKho
    {
        public PhieuKiemKho()
        {
            ChiTietPhieuKiemKho = new HashSet<ChiTietPhieuKiemKho>();
        }

        [Display(Name = "Id", ResourceType = typeof(
                    Resources.DataAnnotations))]
        public int Id { get; set; }

        [Display(Name = "NgayLap", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime NgayLap { get; set; }

        [Display(Name = "NhanVien", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int NhanVienId { get; set; }

        public virtual ICollection<ChiTietPhieuKiemKho> ChiTietPhieuKiemKho { get; set; }
        public virtual Staff NhanVien { get; set; }
    }
}
