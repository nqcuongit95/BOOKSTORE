using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Vanphongpham
    {
        public int MaHangHoa { get; set; }
        public string Ten { get; set; }
        public int MaNsx { get; set; }
        public string MoTa { get; set; }

        public virtual Hanghoa Hanghoa { get; set; }
        public virtual Nhasanxuat MaNsxNavigation { get; set; }
    }
}
