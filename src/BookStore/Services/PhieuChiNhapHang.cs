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
        #region PhieuChiNhapHang
        public IQueryable<PhieuChi> GetPhieuChiNhapHang()
        {
            string sortOrder = null;

            IQueryable<PhieuChi> result = _context.PhieuChi
                .Include(m => m.PhieuNhapHang)
                .ThenInclude(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.PhieuNhapHang.TrangThai)
                .Include(m => m.NhanVien)
                .Include(m => m.LoaiPhieu)
                .Where(m => m.PhieuNhapHang != null &&
                m.LoaiPhieu.Loai == "PCNH");

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<PhieuChi> GetPhieuChiNhapHangById(int? id)
        {
            return await _context.PhieuChi
                .Include(m => m.PhieuNhapHang)
                .ThenInclude(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.PhieuNhapHang.TrangThai)
                .Include(m => m.NhanVien)
                .Include(m => m.LoaiPhieu)
                .Where(m => m.PhieuNhapHang != null &&
                m.LoaiPhieu.Loai == "PCNH")
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public bool PhieuChiNhapHangExists(int? id)
        {
            return _context.PhieuChi.Any(e => e.Id == id);
        }

        public async Task<int> AddPhieuChiNhapHang(PhieuChi phieuChi)
        {
            phieuChi.LoaiPhieuId = (await _context.LoaiPhieu
                .Where(m => m.Loai == "PCNH").SingleOrDefaultAsync()).Id;

            await _context.PhieuChi.AddAsync(phieuChi);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePhieuChiNhapHang(PhieuChi phieuChi)
        {
            _context.Entry(phieuChi).State = EntityState.Modified;
            _context.Entry(phieuChi).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePhieuChiNhapHang(int id)
        {
            var phieuNhapHang = await _context.PhieuChi
                .SingleOrDefaultAsync(m => m.Id == id);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
