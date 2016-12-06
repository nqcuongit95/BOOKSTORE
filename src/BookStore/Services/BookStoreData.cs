using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookStoreData : IBookStoreData
    {
        private BOOKSTOREContext _context;

        public BookStoreData(BOOKSTOREContext context)
        {
            _context = context;
        }

        #region NhaCungCap
        public IQueryable<NhaCungCap> GetAllNhaCungCap()
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
            _context.NhaCungCap.Add(nhaCungCap);

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

        #region NhanHieu
        public IQueryable<NhanHieu> GetAllNhanHieu()
        {
            string sortOrder = null;

            IQueryable<NhanHieu> result = _context.NhanHieu;

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<NhanHieu> GetNhanHieuById(int? id)
        {
            return await _context.NhanHieu.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddNhanHieu(NhanHieu nhanHieu)
        {
            _context.NhanHieu.Add(nhanHieu);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateNhanHieu(NhanHieu nhanHieu)
        {
            _context.Entry(nhanHieu).State = EntityState.Modified;
            _context.Entry(nhanHieu).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public bool NhanHieuExists(int? id)
        {
            return _context.NhanHieu.Any(e => e.Id == id);
        }

        public async Task<int> DeleteNhanHieu(int id)
        {
            var nhanHieu = await _context.NhanHieu
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.NhanHieu.Remove(nhanHieu);

            return await _context.SaveChangesAsync();
        }
        #endregion

        #region HangHoa
        public IQueryable<HangHoa> GetAllHangHoa()
        {
            string sortOrder = null;

            IQueryable<HangHoa> result = _context.HangHoa
                .Include(i => i.LoaiHangHoa)
                .Include(i => i.TrangThai);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<HangHoa> GetHangHoaById(int? id)
        {
            return await _context.HangHoa.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddHangHoa(HangHoa hangHoa)
        {
            _context.HangHoa.Add(hangHoa);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateHangHoa(HangHoa hangHoa)
        {
            _context.Entry(hangHoa).State = EntityState.Modified;
            _context.Entry(hangHoa).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public bool HangHoaExists(int? id)
        {
            return _context.HangHoa.Any(e => e.Id == id);
        }

        public async Task<int> DeleteHangHoa(int id)
        {
            var hangHoa = await _context.HangHoa
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.HangHoa.Remove(hangHoa);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
