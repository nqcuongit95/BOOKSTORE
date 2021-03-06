﻿using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class ChiTietDonHang
    {
        public int Id { get; set; }
        public int DonHangId { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual HangHoa HangHoa { get; set; }
    }
}
