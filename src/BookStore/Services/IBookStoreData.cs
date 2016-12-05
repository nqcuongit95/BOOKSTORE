using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreData
    {
        #region NhaCungCap
        IQueryable<NhaCungCap> GetAllNhaCungCap();
        bool NhaCungCapExists(int? id);
        Task<int> AddNhaCungCap(NhaCungCap nhaCungCap);
        Task<NhaCungCap> GetNhaCungCapById(int? id);
        Task<int> UpdateNhaCungCap(NhaCungCap nhaCungCap);
        Task<int> DeleteNhaCungCap(int id);
        #endregion
    }
}
