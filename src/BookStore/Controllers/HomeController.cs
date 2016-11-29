using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BOOKSTOREContext context = new BOOKSTOREContext();
        public IActionResult Index()
        {
            Khachhang a = new Khachhang { TenKhachHang = "nguyen a", SoDienThoai = "1235465487" };
            context.Add(a);
            context.SaveChanges();
            return View();
        }
        
    }
}
