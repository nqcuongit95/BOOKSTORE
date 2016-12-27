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
        #region ChiTietHangHoa
        public IQueryable<ChiTietHangHoa> GetChiTietHangHoa(int? hangHoaId)
        {
            string sortOrder = null;

            IQueryable<ChiTietHangHoa> result = _context.ChiTietHangHoa;

            if (hangHoaId != null)
                result = result.Where(i => i.HangHoaId == hangHoaId);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<ChiTietHangHoa> GetChiTietHangHoaById(int? id)
        {
            return await _context.ChiTietHangHoa.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddChiTietHangHoa(ChiTietHangHoa chiTietHangHoa)
        {
            await _context.ChiTietHangHoa.AddAsync(chiTietHangHoa);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateChiTietHangHoa(ChiTietHangHoa chiTietHangHoa)
        {
            _context.Entry(chiTietHangHoa).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool ChiTietHangHoaExists(int? id)
        {
            return _context.ChiTietHangHoa.Any((System.Linq.Expressions.Expression<Func<ChiTietHangHoa, bool>>)(e => e.Id == id));
        }

        public async Task<int> DeleteChiTietHangHoa(int id)
        {
            var chiTietHangHoa = await _context.ChiTietHangHoa
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.ChiTietHangHoa.Remove(chiTietHangHoa);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
