using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietPhieuKiemKho
    {
        public int Id { get; set; }
        public int PhieuKiemKhoId { get; set; }
        public int HangHoaId { get; set; }
        public int TonKho { get; set; }
        public int TonKhoThucTe { get; set; }
        public string LyDo { get; set; }

        public virtual HangHoa HangHoa { get; set; }
        public virtual PhieuKiemKho PhieuKiemKho { get; set; }
    }
}
