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

        #region NhanHieu
        IQueryable<NhanHieu> GetAllNhanHieu();
        bool NhanHieuExists(int? id);
        Task<int> AddNhanHieu(NhanHieu nhanHieu);
        Task<NhanHieu> GetNhanHieuById(int? id);
        Task<int> UpdateNhanHieu(NhanHieu nhanHieu);
        Task<int> DeleteNhanHieu(int id);
        #endregion

        #region HangHoa
        IQueryable<HangHoa> GetAllHangHoa();
        bool HangHoaExists(int? id);
        Task<int> AddHangHoa(HangHoa hangHoa);
        Task<HangHoa> GetHangHoaById(int? id);
        Task<int> UpdateHangHoa(HangHoa hangHoa);
        Task<int> DeleteHangHoa(int id);
        #endregion
    }
}
