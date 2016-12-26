using System;
using System.Collections.Generic;

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
        public DateTime? NgayLap { get; set; }
        public string TenHangHoa { get; set; }
        public int LoaiHangHoaId { get; set; }
        public int TonKho { get; set; }
        public int NhaCungCapId { get; set; }
        public int NhanHieuId { get; set; }
        public decimal GiaKhoiTao { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBanSi { get; set; }
        public decimal? GiaBanLe { get; set; }
        public int TrangThaiId { get; set; }
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
