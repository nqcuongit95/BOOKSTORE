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
        #region ChiTietPhieuNhapHang
        public IQueryable<ChiTietPhieuNhapHang> GetChiTietPhieuNhapHang(int? phieuNhapHangId)
        {
            string sortOrder = null;

            IQueryable<ChiTietPhieuNhapHang> result =
                _context.ChiTietPhieuNhapHang.Include(i => i.HangHoa);

            if (phieuNhapHangId != null)
                result = result.Where(i => i.PhieuNhapHangId == phieuNhapHangId);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<ChiTietPhieuNhapHang> GetChiTietPhieuNhapHangById(int? id)
        {
            return await _context.ChiTietPhieuNhapHang
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddChiTietPhieuNhapHang(ChiTietPhieuNhapHang thuocTinhHangHoa)
        {
            await _context.ChiTietPhieuNhapHang.AddAsync(thuocTinhHangHoa);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateChiTietPhieuNhapHang(ChiTietPhieuNhapHang thuocTinhHangHoa)
        {
            _context.Entry(thuocTinhHangHoa).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool ChiTietPhieuNhapHangExists(int? id)
        {
            return _context.ChiTietPhieuNhapHang.Any(e => e.Id == id);
        }

        public async Task<int> DeleteChiTietPhieuNhapHang(int id)
        {
            var thuocTinhHangHoa = await _context.ChiTietPhieuNhapHang
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.ChiTietPhieuNhapHang.Remove(thuocTinhHangHoa);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
