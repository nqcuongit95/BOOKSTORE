using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class NhanHieu
    {
        public NhanHieu()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        public int Id { get; set; }
        public string TenNhanHieu { get; set; }

        public virtual ICollection<HangHoa> HangHoa { get; set; }
    }
}
