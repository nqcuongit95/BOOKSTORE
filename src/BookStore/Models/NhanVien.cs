using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace BookStore.Models
{
    public class NhanVien : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgayLamViec { get; set; }
    }
}
