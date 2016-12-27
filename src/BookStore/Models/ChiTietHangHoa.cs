using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietHangHoa
    {
        public int Id { get; set; }
        public int HangHoaId { get; set; }
        public string ThuocTinh { get; set; }
        public string GiaTri { get; set; }

        public virtual HangHoa HangHoa { get; set; }
    }
}
