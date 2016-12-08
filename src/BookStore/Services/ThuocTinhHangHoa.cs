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
        #region ThuocTinhHangHoa
        public IQueryable<ThuocTinhHangHoa> GetThuocTinhHangHoa(int? loaiHangHoaId)
        {
            string sortOrder = null;

            IQueryable<ThuocTinhHangHoa> result = _context.ThuocTinhHangHoa;

            if (loaiHangHoaId != null)
                result = result.Where(i => i.LoaiHangHoaId == loaiHangHoaId);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<ThuocTinhHangHoa> GetThuocTinhHangHoaById(int? id)
        {
            return await _context.ThuocTinhHangHoa.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddThuocTinhHangHoa(ThuocTinhHangHoa nhanHieu)
        {
            await _context.ThuocTinhHangHoa.AddAsync(nhanHieu);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateThuocTinhHangHoa(ThuocTinhHangHoa nhanHieu)
        {
            _context.Entry(nhanHieu).State = EntityState.Modified;
            _context.Entry(nhanHieu).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public bool ThuocTinhHangHoaExists(int? id)
        {
            return _context.ThuocTinhHangHoa.Any(e => e.Id == id);
        }

        public async Task<int> DeleteThuocTinhHangHoa(int id)
        {
            var nhanHieu = await _context.ThuocTinhHangHoa
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.ThuocTinhHangHoa.Remove(nhanHieu);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
