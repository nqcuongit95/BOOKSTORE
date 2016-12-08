using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            DonHang = new HashSet<DonHang>();
            PhieuChi = new HashSet<PhieuChi>();
            PhieuKiemKho = new HashSet<PhieuKiemKho>();
            PhieuNhapHang = new HashSet<PhieuNhapHang>();
            PhieuThu = new HashSet<PhieuThu>();
            PhieuTraHang = new HashSet<PhieuTraHang>();
            PhieuTraNhapHang = new HashSet<PhieuTraNhapHang>();
        }
        public string FullName { get; set; }
        public DateTime? DateCreate { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<PhieuChi> PhieuChi { get; set; }
        public virtual ICollection<PhieuThu> PhieuThu { get; set; }
        public virtual ICollection<PhieuTraNhapHang> PhieuTraNhapHang { get; set; }
        public virtual ICollection<PhieuKiemKho> PhieuKiemKho { get; set; }
        public virtual ICollection<PhieuNhapHang> PhieuNhapHang { get; set; }
        public virtual ICollection<PhieuTraHang> PhieuTraHang { get; set; }        

    }
}
