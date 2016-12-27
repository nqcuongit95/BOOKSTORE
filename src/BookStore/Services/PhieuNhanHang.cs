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
        #region PhieuNhanHang
        public IQueryable<PhieuNhanHang> GetPhieuNhanHang()
        {
            string sortOrder = null;

            IQueryable<PhieuNhanHang> result = _context.PhieuNhanHang
                .Include(m => m.PhieuNhapHang)
                .ThenInclude(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.NhanVien);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<PhieuNhanHang> GetPhieuNhanHangById(int? id)
        {
            return await _context.PhieuNhanHang
                .Include(m => m.PhieuNhapHang)
                .ThenInclude(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.NhanVien)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public bool PhieuNhanHangExists(int? id)
        {
            return _context.PhieuNhanHang.Any(e => e.Id == id);
        }

        public async Task<int> AddPhieuNhanHang(PhieuNhanHang phieuNhanHang)
        {
            await _context.PhieuNhanHang.AddAsync(phieuNhanHang);

            return await RecivePhieuNhapHang(phieuNhanHang.PhieuNhapHangId);
        }

        public async Task<int> UpdatePhieuNhanHang(PhieuNhanHang phieuNhanHang)
        {
            _context.Entry(phieuNhanHang).State = EntityState.Modified;
            _context.Entry(phieuNhanHang).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePhieuNhanHang(int id)
        {
            var phieuNhapHang = await _context.PhieuNhanHang
                .SingleOrDefaultAsync(m => m.Id == id);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
