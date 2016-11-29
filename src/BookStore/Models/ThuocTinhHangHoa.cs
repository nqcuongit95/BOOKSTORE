using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ThuocTinhHangHoa
    {
        public int Id { get; set; }
        public int LoaiHangHoaId { get; set; }
        public string TenThuocTinh { get; set; }

        public virtual LoaiHangHoa LoaiHangHoa { get; set; }
    }
}
