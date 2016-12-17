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
        IQueryable<DonHangViewModel> GetAllPhieuChi();
        int TaoDonHang(DonHang donhang);
    }
}
