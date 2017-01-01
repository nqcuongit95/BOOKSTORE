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
        #region HangHoa

        public IQueryable<HangHoa> GetHangHoa(string search, string state)
        {
            string sortOrder = null;

            IQueryable<HangHoa> result = _context.HangHoa
                .Include(m => m.LoaiHangHoa)
                .Include(m => m.ChiTietHangHoa)
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "HangHoa");

            if (!String.IsNullOrEmpty(search))
                result = result.Where(i => i.TenHangHoa.Contains(search));

            switch (state)
            {
                case "Use":
                    result = result.Where(i => i.TrangThai.VietTat == "Use");
                    break;

                case "NotUse":
                    result = result.Where(i => i.TrangThai.VietTat == "NotUse");
                    break;
            }

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
            return await _context.HangHoa
                .Include(m => m.LoaiHangHoa)
                .Include(m => m.ChiTietHangHoa)
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "HangHoa")
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public bool HangHoaExists(int? id)
        {
            return _context.HangHoa.Any(e => e.Id == id);
        }

        public async Task<int> AddHangHoa(HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties)
        {
            hangHoa.TrangThai = await _context.TrangThai
                .SingleOrDefaultAsync(m => m.VietTat == hangHoa.TrangThai.VietTat
                 && m.Loai == hangHoa.TrangThai.Loai);
            hangHoa.TrangThaiId = hangHoa.TrangThai.Id;

            await _context.HangHoa.AddAsync(hangHoa);

            foreach (var property in properties)
                property.HangHoaId = hangHoa.Id;

            await _context.ChiTietHangHoa.AddRangeAsync(properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateHangHoa(HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties)
        {
            _context.Entry(hangHoa).State = EntityState.Modified;
            _context.Entry(hangHoa).Property("NgayLap").IsModified = false;

            _context.ChiTietHangHoa.RemoveRange(await _context.ChiTietHangHoa
                .Where(i => i.HangHoaId == hangHoa.Id).ToArrayAsync());

            foreach (var property in properties)
                property.HangHoaId = hangHoa.Id;
            await _context.ChiTietHangHoa.AddRangeAsync(properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHangHoa(int id)
        {
            var hangHoa = await _context.HangHoa
                .SingleOrDefaultAsync(m => m.Id == id);

            var properties = _context.ChiTietHangHoa
                .Where(i => i.HangHoaId == hangHoa.Id);

            _context.ChiTietHangHoa.RemoveRange(properties);
            _context.HangHoa.Remove(hangHoa);

            return await _context.SaveChangesAsync();
        }

        #endregion HangHoa
    }
}