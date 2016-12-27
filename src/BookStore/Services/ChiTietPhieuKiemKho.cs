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
        #region ChiTietPhieuKiemKho
        public IQueryable<ChiTietPhieuKiemKho> GetChiTietPhieuKiemKho(
            int? phieuKiemKhoId)
        {
            string sortOrder = null;

            IQueryable<ChiTietPhieuKiemKho> result =
                _context.ChiTietPhieuKiemKho.Include(i => i.HangHoa);

            if (phieuKiemKhoId != null)
                result = result.Where(i => i.PhieuKiemKhoId == phieuKiemKhoId);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<ChiTietPhieuKiemKho> GetChiTietPhieuKiemKhoById(int? id)
        {
            return await _context.ChiTietPhieuKiemKho
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddChiTietPhieuKiemKho(ChiTietPhieuKiemKho thuocTinhHangHoa)
        {
            await _context.ChiTietPhieuKiemKho.AddAsync(thuocTinhHangHoa);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateChiTietPhieuKiemKho(ChiTietPhieuKiemKho thuocTinhHangHoa)
        {
            _context.Entry(thuocTinhHangHoa).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool ChiTietPhieuKiemKhoExists(int? id)
        {
            return _context.ChiTietPhieuKiemKho.Any(e => e.Id == id);
        }

        public async Task<int> DeleteChiTietPhieuKiemKho(int id)
        {
            var thuocTinhHangHoa = await _context.ChiTietPhieuKiemKho
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.ChiTietPhieuKiemKho.Remove(thuocTinhHangHoa);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
