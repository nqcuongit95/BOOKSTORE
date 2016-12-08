using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class HangHoa
    {
        public HangHoa()
        {
            ChiTietDonHang = new HashSet<ChiTietDonHang>();
            ChiTietHangHoa = new HashSet<ChiTietHangHoa>();
            ChiTietPhieuKiemKho = new HashSet<ChiTietPhieuKiemKho>();
            ChiTietPhieuNhapHang = new HashSet<ChiTietPhieuNhapHang>();
        }

        public int Id { get; set; }

        [Display(Name = "TenHangHoa", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string TenHangHoa { get; set; }

        [Display(Name = "NgayLap", ResourceType = typeof(Resources.DataAnnotations))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "LoaiHangHoa", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int LoaiHangHoaId { get; set; }

        [Display(Name = "TonKho", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public int TonKho { get; set; }

        [Display(Name = "NhaCungCap", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int NhaCungCapId { get; set; }

        [Display(Name = "NhanHieu", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        public int NhanHieuId { get; set; }

        [Display(Name = "GiaKhoiTao", ResourceType = typeof(
            Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Bạn không được để trống trường này.")]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]        
        public decimal GiaKhoiTao { get; set; }

        [Display(Name = "GiaNhap", ResourceType = typeof(
            Resources.DataAnnotations))]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public decimal? GiaNhap { get; set; }

        [Display(Name = "GiaBanSi", ResourceType = typeof(
            Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public decimal? GiaBanSi { get; set; }

        [Display(Name = "GiaBanLe", ResourceType = typeof(
            Resources.DataAnnotations))]
        [DataType(DataType.Currency)]
        [Range(0, 1000000,
            ErrorMessage = "Giá trị của trường tối thiểu {1} và không vượt quá {2}.")]
        public decimal? GiaBanLe { get; set; }

        [Display(Name = "TrangThai", ResourceType = typeof(
            Resources.DataAnnotations))]
        public int TrangThaiId { get; set; }

        [Display(Name = "DaBan", ResourceType = typeof(
            Resources.DataAnnotations))]
        public int? DaBan { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual ICollection<ChiTietHangHoa> ChiTietHangHoa { get; set; }
        public virtual ICollection<ChiTietPhieuKiemKho> ChiTietPhieuKiemKho { get; set; }
        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual LoaiHangHoa LoaiHangHoa { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual NhanHieu NhanHieu { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}
