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

        public void CreateCustomer(KhachHang customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public IQueryable<CustomerInfoViewModel> GetAllKhachHang()
        {
            var query = from customer in _context.KhachHang
                        join type in _context.LoaiKhachHang
                        on customer.LoaiKhachHangId equals type.Id
                        select new CustomerInfoViewModel
                        {
                            TenKhachHang = customer.TenKhachHang,
                            SoDienThoai = customer.SoDienThoai,
                            Email = customer.Email,
                            TenLoaiKhachHang = type.TenLoaiKhachHang,
                            NgayLap = customer.NgayLap
                        };

            return query;
        }

        public IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang()
        {
            return _context.LoaiKhachHang;
        }

        public string GetTenLoaiKhachHang(int id)
        {
            return _context.LoaiKhachHang.First(i => i.Id == id).TenLoaiKhachHang;
        }
    }
}
