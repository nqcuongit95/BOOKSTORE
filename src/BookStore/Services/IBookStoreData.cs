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
        IQueryable<CustomerInfoViewModel> GetAllKhachHang();

        void CreateCustomer(KhachHang customer);

        IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang();

        string GetTenLoaiKhachHang(int id);

        CustomerInfoViewModel GetKhachHang(int id);

    }
}
