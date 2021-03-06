﻿using BookStore.Models;
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
            return await _context.ThuocTinhHangHoa
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddThuocTinhHangHoa(ThuocTinhHangHoa thuocTinhHangHoa)
        {
            await _context.ThuocTinhHangHoa.AddAsync(thuocTinhHangHoa);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateThuocTinhHangHoa(ThuocTinhHangHoa thuocTinhHangHoa)
        {
            _context.Entry(thuocTinhHangHoa).State = EntityState.Modified;
            _context.Entry(thuocTinhHangHoa).Property("NgayLap").IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public bool ThuocTinhHangHoaExists(int? id)
        {
            return _context.ThuocTinhHangHoa.Any(e => e.Id == id);
        }

        public async Task<int> DeleteThuocTinhHangHoa(int id)
        {
            var thuocTinhHangHoa = await _context.ThuocTinhHangHoa
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.ThuocTinhHangHoa.Remove(thuocTinhHangHoa);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
