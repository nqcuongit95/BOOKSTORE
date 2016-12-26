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
        public IQueryable<PhieuNhapHang> GetPhieuNhapHang(string trangThai)
        {
            string sortOrder = null;

            IQueryable<PhieuNhapHang> result = _context.PhieuNhapHang
                .Include(m => m.NhaCungCap)
                .Include(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "PhieuNhapHang")
                .Include(m => m.NhanVien);

            if (trangThai != null)
                result = result.Where(m => m.TrangThai.VietTat == trangThai);

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
            return await _context.PhieuNhapHang
                .Include(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.NhaCungCap)
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "PhieuNhapHang")
                .Include(m => m.NhanVien)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public bool PhieuNhapHangExists(int? id)
        {
            return _context.PhieuNhapHang.Any(e => e.Id == id);
        }

        public async Task<int> AddPhieuNhapHang(PhieuNhapHang phieuNhapHang,
            ICollection<ChiTietPhieuNhapHang> properties)
        {
            phieuNhapHang.TrangThaiId =
                (await GetTrangThai("Waiting", "PhieuNhapHang")
                .FirstOrDefaultAsync()).Id;

            await _context.PhieuNhapHang.AddAsync(phieuNhapHang);

            foreach (var property in properties)
            {
                property.PhieuNhapHangId = phieuNhapHang.Id;

                phieuNhapHang.TongTien = property.SoLuong * property.GiaNhap;
            }

            await _context.ChiTietPhieuNhapHang.AddRangeAsync(properties);

            PhieuChi phieuChi = new PhieuChi()
            {
                NhanVienId = phieuNhapHang.NhanVienId,
                NgayLap = phieuNhapHang.NgayLap,
                PhieuNhapHangId = phieuNhapHang.Id,
                TongTien = phieuNhapHang.TongTien,
            };

            return await AddPhieuChiNhapHang(phieuChi);
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

        public IQueryable<PhieuNhapHang> GetUnrecivedPhieuNhapHang()
        {
            string sortOrder = null;

            IQueryable<PhieuNhapHang> result = _context.PhieuNhapHang
                .Include(m => m.NhaCungCap)
                .Include(m => m.ChiTietPhieuNhapHang)
                .ThenInclude(m => m.HangHoa)
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "PhieuNhapHang"
                && (m.TrangThai.VietTat != "Recived" ||
                m.TrangThai.VietTat != "PaidRecived"))
                .Include(m => m.NhanVien);

            switch (sortOrder)
            {
                default:
                    result = result.OrderBy(i => i.Id);
                    break;
            }

            return result.AsNoTracking();
        }

        public async Task<int> PayPhieuNhapHang(int id)
        {
            var phieuNhapHang = await _context.PhieuNhapHang
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "PhieuNhapHang")
                .SingleOrDefaultAsync(m => m.Id == id);

            if (phieuNhapHang.TrangThai.VietTat != "Paid" ||
                phieuNhapHang.TrangThai.VietTat != "PaidRecived")
            {
                if (phieuNhapHang.TrangThai.VietTat == "Recived")
                    phieuNhapHang.TrangThaiId =
                        (await _context.TrangThai.SingleOrDefaultAsync(
                            m => m.VietTat == "PaidRecived" &&
                            m.Loai == "PhieuNhapHang")).Id;
                else
                    phieuNhapHang.TrangThaiId =
                        (await _context.TrangThai.Where(
                            m => m.VietTat == "Paid" &&
                            m.Loai == "PhieuNhapHang").FirstOrDefaultAsync()).Id;

                _context.PhieuNhapHang.Attach(phieuNhapHang);
                _context.Entry(phieuNhapHang)
                    .Property(m => m.TrangThaiId).IsModified = true;
            }
            else
                throw new Exception("Invalid");

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RecivePhieuNhapHang(int id)
        {
            var phieuNhapHang = await _context.PhieuNhapHang
                .Include(m => m.TrangThai)
                .Where(m => m.TrangThai.Loai == "PhieuNhapHang")
                .SingleOrDefaultAsync(m => m.Id == id);

            if (phieuNhapHang.TrangThai.VietTat != "Recived" ||
                phieuNhapHang.TrangThai.VietTat != "PaidRecived")
            {
                if (phieuNhapHang.TrangThai.VietTat == "Paid")
                    phieuNhapHang.TrangThaiId =
                        (await _context.TrangThai.SingleOrDefaultAsync(
                            m => m.VietTat == "PaidRecived" &&
                            m.Loai == "PhieuNhapHang")).Id;
                else
                    phieuNhapHang.TrangThaiId =
                        (await _context.TrangThai.SingleOrDefaultAsync(
                            m => m.VietTat == "Recived" &&
                            m.Loai == "PhieuNhapHang")).Id;

                _context.PhieuNhapHang.Attach(phieuNhapHang);
                _context.Entry(phieuNhapHang)
                    .Property(m => m.TrangThaiId).IsModified = true;
            }
            else
                throw new Exception("Invalid");

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
