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

        public IEnumerable<KhachHang> GetAllCustomer()
        {
            return _context.KhachHang;
        }

        public List<object> GetAllDonHang()
        {
            //return _context.DonHang;

            var list = (from dh in _context.DonHang
                        join kh in _context.KhachHang
                        on dh.KhachHangId equals kh.Id
                        join trangthai in _context.TrangThai
                        on dh.TrangThaiId equals trangthai.Id
                        select new DonHangViewModels
                        {
                            NgayLap = dh.NgayLap,
                            KhachHangID = kh.Id,
                            KhachHang = kh.TenKhachHang,
                            TrangThaiID = trangthai.Id,
                            TrangThai = trangthai.TenTrangThai,
                            TongTien = dh.TongTien
                        }).ToList();
            return new List<object>(list);
        }

        public IEnumerable<DonHang> GetDonHang()
        {
            return _context.DonHang;
        }
    }
}
