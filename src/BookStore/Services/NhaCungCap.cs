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
        #region NhaCungCap
        public IQueryable<NhaCungCap> GetNhaCungCap()
        {
            string sortOrder = null;

            IQueryable<NhaCungCap> result = _context.NhaCungCap;

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<NhaCungCap> GetNhaCungCapById(int? id)
        {
            return await _context.NhaCungCap.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddNhaCungCap(NhaCungCap nhaCungCap)
        {
            await _context.NhaCungCap.AddAsync(nhaCungCap);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateNhaCungCap(NhaCungCap nhaCungCap)
        {
            _context.Entry(nhaCungCap).State = EntityState.Modified;
            _context.Entry(nhaCungCap).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public bool NhaCungCapExists(int? id)
        {
            return _context.NhaCungCap.Any(e => e.Id == id);
        }

        public async Task<int> DeleteNhaCungCap(int id)
        {
            var nhaCungCap = await _context.NhaCungCap
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.NhaCungCap.Remove(nhaCungCap);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
