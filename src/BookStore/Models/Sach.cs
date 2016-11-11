using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Sach
    {
        public int MaHangHoa { get; set; }
        public string TenSach { get; set; }
        public int MaTacGia { get; set; }
        public int MaNxb { get; set; }
        public int MaLoaiSach { get; set; }
        public string Isbn { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayXuatBan { get; set; }

        public virtual Hanghoa Hanghoa { get; set; }
        public virtual Theloaisach MaLoaiSachNavigation { get; set; }
        public virtual Nhaxuatban MaNxbNavigation { get; set; }
        public virtual Tacgia MaTacGiaNavigation { get; set; }
    }
}
