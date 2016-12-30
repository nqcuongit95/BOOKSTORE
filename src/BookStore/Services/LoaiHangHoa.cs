using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public partial class BookStoreData
    {
        #region LoaiHangHoa
        public IQueryable<LoaiHangHoa> GetLoaiHangHoa()
        {
            string sortOrder = null;

            IQueryable<LoaiHangHoa> result = _context.LoaiHangHoa;

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<LoaiHangHoa> GetLoaiHangHoaById(int? id)
        {
            return await _context.LoaiHangHoa.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddLoaiHangHoa(LoaiHangHoa loaiHangHoa)
        {
            await _context.LoaiHangHoa.AddAsync(loaiHangHoa);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateLoaiHangHoa(LoaiHangHoa loaiHangHoa)
        {
            _context.Entry(loaiHangHoa).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool LoaiHangHoaExists(int? id)
        {
            return _context.LoaiHangHoa.Any(e => e.Id == id);
        }

        public async Task<int> DeleteLoaiHangHoa(int id)
        {
            var loaiHangHoa = await _context.LoaiHangHoa
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.LoaiHangHoa.Remove(loaiHangHoa);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
