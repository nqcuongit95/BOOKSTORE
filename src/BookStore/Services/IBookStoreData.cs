using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreData
    {
        #region TrangThai
        IQueryable<TrangThai> GetTrangThai(string loai);
        bool TrangThaiExists(int? id);
        Task<int> AddTrangThai(TrangThai trangThai);
        Task<TrangThai> GetTrangThaiById(int? id);
        Task<int> UpdateTrangThai(TrangThai trangThai);
        Task<int> DeleteTrangThai(int id);
        #endregion

        #region NhaCungCap
        IQueryable<NhaCungCap> GetNhaCungCap();
        bool NhaCungCapExists(int? id);
        Task<int> AddNhaCungCap(NhaCungCap nhaCungCap);
        Task<NhaCungCap> GetNhaCungCapById(int? id);
        Task<int> UpdateNhaCungCap(NhaCungCap nhaCungCap);
        Task<int> DeleteNhaCungCap(int id);
        #endregion

        #region NhanHieu
        IQueryable<NhanHieu> GetNhanHieu();
        bool NhanHieuExists(int? id);
        Task<int> AddNhanHieu(NhanHieu nhanHieu);
        Task<NhanHieu> GetNhanHieuById(int? id);
        Task<int> UpdateNhanHieu(NhanHieu nhanHieu);
        Task<int> DeleteNhanHieu(int id);
        #endregion

        #region LoaiHangHoa
        IQueryable<LoaiHangHoa> GetLoaiHangHoa();
        Task<LoaiHangHoa> GetLoaiHangHoaById(int? id);
        bool LoaiHangHoaExists(int? id);
        Task<int> AddLoaiHangHoa(LoaiHangHoa loaiHangHoa);
        Task<int> UpdateLoaiHangHoa(LoaiHangHoa loaiHangHoa);
        Task<int> DeleteLoaiHangHoa(int id);
        #endregion

        #region ThuocTinhHangHoa
        IQueryable<ThuocTinhHangHoa> GetThuocTinhHangHoa(int? loaiHangHoaId);
        bool ThuocTinhHangHoaExists(int? id);
        Task<int> AddThuocTinhHangHoa(ThuocTinhHangHoa nhanHieu);
        Task<ThuocTinhHangHoa> GetThuocTinhHangHoaById(int? id);
        Task<int> UpdateThuocTinhHangHoa(ThuocTinhHangHoa nhanHieu);
        Task<int> DeleteThuocTinhHangHoa(int id);
        #endregion

        #region ChiTietHangHoa
        IQueryable<ChiTietHangHoa> GetChiTietHangHoa(int? hangHoaId);
        bool ChiTietHangHoaExists(int? id);
        Task<int> AddChiTietHangHoa(ChiTietHangHoa chiTietHangHoa);
        Task<ChiTietHangHoa> GetChiTietHangHoaById(int? id);
        Task<int> UpdateChiTietHangHoa(ChiTietHangHoa chiTietHangHoa);
        Task<int> DeleteChiTietHangHoa(int id);
        #endregion

        #region HangHoa
        IQueryable<HangHoa> GetHangHoa();
        bool HangHoaExists(int? id);
        Task<int> AddHangHoa(HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties);
        Task<HangHoa> GetHangHoaById(int? id);
        Task<int> UpdateHangHoa(HangHoa hangHoa,
           ICollection<ChiTietHangHoa> properties);
        Task<int> DeleteHangHoa(int id);
        #endregion
    }
}
