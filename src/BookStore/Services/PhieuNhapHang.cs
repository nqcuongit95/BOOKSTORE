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
        #region PhieuNhapHang
        public IQueryable<PhieuNhapHang> GetPhieuNhapHang()
        {
            string sortOrder = null;

            IQueryable<PhieuNhapHang> result = _context.PhieuNhapHang;

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<PhieuNhapHang> GetPhieuNhapHangById(int? id)
        {
            return await _context.PhieuNhapHang.SingleOrDefaultAsync(m => m.Id == id);
        }

        public bool PhieuNhapHangExists(int? id)
        {
            return _context.PhieuNhapHang.Any(e => e.Id == id);
        }

        public async Task<int> AddPhieuNhapHang(PhieuNhapHang phieuNhapHang,
            ICollection<ChiTietPhieuNhapHang> properties)
        {
            await _context.PhieuNhapHang.AddAsync(phieuNhapHang);

            foreach (var property in properties)
            {
                property.PhieuNhapHangId = phieuNhapHang.Id;
                var hangHoa = await _context.HangHoa
                    .SingleOrDefaultAsync(i => i.Id == property.HangHoaId);
                property.NhaCungCapId = hangHoa.NhaCungCapId;

                phieuNhapHang.TongTien = property.SoLuong * property.GiaNhap;
            }

            await _context.ChiTietPhieuNhapHang.AddRangeAsync(properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePhieuNhapHang(PhieuNhapHang phieuNhapHang,
            ICollection<ChiTietPhieuNhapHang> properties)
        {
            _context.Entry(phieuNhapHang).State = EntityState.Modified;
            _context.Entry(phieuNhapHang).Property("NgayLap").IsModified = false;

            _context.ChiTietPhieuNhapHang.RemoveRange(await _context.ChiTietPhieuNhapHang
                .Where(i => i.PhieuNhapHangId == phieuNhapHang.Id).ToArrayAsync());

            foreach (var property in properties)
                property.PhieuNhapHangId = phieuNhapHang.Id;
            await _context.ChiTietPhieuNhapHang.AddRangeAsync(properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePhieuNhapHang(int id)
        {
            var phieuNhapHang = await _context.PhieuNhapHang
                .SingleOrDefaultAsync(m => m.Id == id);

            var properties = _context.ChiTietPhieuNhapHang
                .Where(i => i.PhieuNhapHangId == phieuNhapHang.Id);

            _context.ChiTietPhieuNhapHang.RemoveRange(properties);
            _context.PhieuNhapHang.Remove(phieuNhapHang);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
