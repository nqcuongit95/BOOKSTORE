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
        #region PhieuKiemKho
        public IQueryable<PhieuKiemKho> GetPhieuKiemKho()
        {
            string sortOrder = null;

            IQueryable<PhieuKiemKho> result = _context.PhieuKiemKho
                .Include(m => m.NhanVien);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<PhieuKiemKho> GetPhieuKiemKhoById(int? id)
        {
            return await _context.PhieuKiemKho
                .Include(m => m.NhanVien)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public bool PhieuKiemKhoExists(int? id)
        {
            return _context.PhieuKiemKho.Any(e => e.Id == id);
        }

        public async Task<int> AddPhieuKiemKho(PhieuKiemKho phieuKiemKho,
            ICollection<ChiTietPhieuKiemKho> properties)
        {
            await _context.PhieuKiemKho.AddAsync(phieuKiemKho);

            foreach (var property in properties)
            {
                var hangHoa = await _context.HangHoa
                    .SingleOrDefaultAsync(m => m.Id == property.HangHoaId);

                property.PhieuKiemKhoId = phieuKiemKho.Id;
                property.TonKho = hangHoa.TonKho;
                hangHoa.TonKho = property.TonKhoThucTe;

                _context.HangHoa.Update(hangHoa);
            }

            await _context.ChiTietPhieuKiemKho.AddRangeAsync(properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePhieuKiemKho(PhieuKiemKho phieuKiemKho,
            ICollection<ChiTietPhieuKiemKho> properties)
        {
            _context.Entry(phieuKiemKho).State = EntityState.Modified;
            _context.Entry(phieuKiemKho).Property("NgayLap").IsModified = false;

            _context.ChiTietPhieuKiemKho.RemoveRange(await _context.ChiTietPhieuKiemKho
                .Where(i => i.PhieuKiemKhoId == phieuKiemKho.Id).ToArrayAsync());

            foreach (var property in properties)
                property.PhieuKiemKhoId = phieuKiemKho.Id;
            await _context.ChiTietPhieuKiemKho.AddRangeAsync(properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePhieuKiemKho(int id)
        {
            var phieuKiemKho = await _context.PhieuKiemKho
                .SingleOrDefaultAsync(m => m.Id == id);

            var properties = _context.ChiTietPhieuKiemKho
                .Where(i => i.PhieuKiemKhoId == phieuKiemKho.Id);

            _context.ChiTietPhieuKiemKho.RemoveRange(properties);
            _context.PhieuKiemKho.Remove(phieuKiemKho);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
