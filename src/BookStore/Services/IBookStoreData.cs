using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreData
    {
        IQueryable<DonHangViewModel> GetAllDonHang();
        IQueryable<PhieuThuViewModel> GetAllPhieuThu();
        IQueryable<PhieuChiViewModel> GetAllPhieuChi();
        IQueryable<LoaiPhieu> GetAllLoaiPhieu();
        IQueryable<PhieuTraNhapHang> GetAllPhieuTraNhapHang();
        int TaoDonHang(DonHang donhang);
        int TaoPhieuThu(PhieuThu phieuthu);
        int TaoPhieuChi(PhieuChi phieuchi);
    }
}
