using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookStoreData : IBookStoreData
    {
        private BOOKSTOREContext _context;

        public BookStoreData(BOOKSTOREContext context)
        {
            _context = context;
        }

        public IQueryable<DonHangViewModel> GetAllDonHang()
        {
            var query = from donhang in _context.DonHang
                        join kh in _context.KhachHang
                        on donhang.KhachHangId equals kh.Id
                        join trangthai in _context.TrangThai
                        on donhang.TrangThaiId equals trangthai.Id
                        select new DonHangViewModel
                        {
                            ID = donhang.Id,
                            TenKhachHang = kh.TenKhachHang,
                            NgayLap = donhang.NgayLap,
                            KhachHangId = kh.Id,
                            TrangThaiId = trangthai.Id,
                            TenTrangThai = trangthai.TenTrangThai,
                            TongTien = donhang.TongTien,

                        };

            return query;
        }
    }
}
