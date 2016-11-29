using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class LoaiHangHoa
    {
        public LoaiHangHoa()
        {
            HangHoa = new HashSet<HangHoa>();
            ThuocTinhHangHoa = new HashSet<ThuocTinhHangHoa>();
        }

        public int Id { get; set; }
        public string TenLoaiHangHoa { get; set; }

        public virtual ICollection<HangHoa> HangHoa { get; set; }
        public virtual ICollection<ThuocTinhHangHoa> ThuocTinhHangHoa { get; set; }
    }
}
