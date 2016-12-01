using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            //var c = _context.DonHang.Include(i => i.TrangThai)
            return _context.KhachHang;
        }

        public IEnumerable<DonHang> GetDonHang()
        {
            var viewmodel = _context.DonHang
                .Include(i => i.KhachHang)
                .AsNoTracking();
            //return _context.DonHang
            //    .Include(i => i.KhachHang)
            //    .Include(j => j.TrangThai)
            //    .AsNoTracking();
            return viewmodel;
        }
    }
}
