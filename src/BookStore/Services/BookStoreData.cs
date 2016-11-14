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

        public IEnumerable<Khachhang> GetAllCustomer()
        {
            return _context.Khachhang;
        }
    }
}
