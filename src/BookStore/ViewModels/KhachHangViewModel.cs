using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class KhachHangViewModel
    {
        public KhachHang KhachHang { get; set; }
        public SelectList LoaiKhachHang { get; set; }
    }
}
