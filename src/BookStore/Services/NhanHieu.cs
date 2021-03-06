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
        #region NhanHieu
        public IQueryable<NhanHieu> GetNhanHieu()
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
            return await _context.NhanHieu.SingleOrDefaultAsync((System.Linq.Expressions.Expression<Func<NhanHieu, bool>>)(m => m.Id == id));
        }

        public async Task<int> AddNhanHieu(NhanHieu nhanHieu)
        {
            await _context.NhanHieu.AddAsync(nhanHieu);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateNhanHieu(NhanHieu nhanHieu)
        {
            _context.Entry(nhanHieu).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool NhanHieuExists(int? id)
        {
            return _context.NhanHieu.Any((System.Linq.Expressions.Expression<Func<NhanHieu, bool>>)(e => e.Id == id));
        }

        public async Task<int> DeleteNhanHieu(int id)
        {
            var nhanHieu = await _context.NhanHieu
                .SingleOrDefaultAsync((System.Linq.Expressions.Expression<Func<NhanHieu, bool>>)(m => m.Id == id));
            _context.NhanHieu.Remove((NhanHieu)nhanHieu);

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
