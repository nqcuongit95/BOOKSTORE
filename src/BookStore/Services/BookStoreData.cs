using BookStore.Models;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<NhaCungCap> GetAllNhaCungCap()
        {
            return _context.NhaCungCap.AsNoTracking();
        }

        public Task<int> CountNhaCungCap()
        {
            return _context.NhaCungCap.CountAsync();
        }
    }
}
