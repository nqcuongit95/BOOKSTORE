using BookStore.Models;
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

        public IEnumerable<KhachHang> GetAllCustomer()
        {
            return _context.KhachHang;
        }

        public IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang()
        {
            return _context.LoaiKhachHang;
        }
    }
}
