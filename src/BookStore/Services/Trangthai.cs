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
        #region TrangThai
        public IQueryable<TrangThai> GetTrangThai(string vietTat, string loai)
        {
            string sortOrder = null;

            IQueryable<TrangThai> result = _context.TrangThai;

            if (vietTat != null)
                result = result.Where(i => i.VietTat == vietTat);

            if (loai != null)
                result = result.Where(i => i.Loai == loai);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<TrangThai> GetTrangThaiById(int? id)
        {
            return await _context.TrangThai.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddTrangThai(TrangThai trangThai)
        {
            await _context.TrangThai.AddAsync(trangThai);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTrangThai(TrangThai trangThai)
        {
            _context.Entry(trangThai).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool TrangThaiExists(int? id)
        {
            return _context.TrangThai.Any((System.Linq.Expressions.Expression<Func<TrangThai, bool>>)(e => e.Id == id));
        }

        public async Task<int> DeleteTrangThai(int id)
        {
            var trangThai = await _context.TrangThai
                .SingleOrDefaultAsync((System.Linq.Expressions.Expression<Func<TrangThai, bool>>)(m => m.Id == id));
            _context.TrangThai.Remove((TrangThai)trangThai);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
