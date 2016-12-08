using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class User : IdentityUser
    {
        public User()
        {           
            DonHang = new HashSet<DonHang>();
            PhieuChi = new HashSet<PhieuChi>();
            PhieuThu = new HashSet<PhieuThu>();
            PhieuTraNhapHang = new HashSet<PhieuTraNhapHang>();
        }
        public string HoTen { get; set; }
        public DateTime NgayLamViec { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<PhieuChi> PhieuChi { get; set; }
        public virtual ICollection<PhieuThu> PhieuThu { get; set; }
        public virtual ICollection<PhieuTraNhapHang> PhieuTraNhapHang { get; set; }
    }
}
