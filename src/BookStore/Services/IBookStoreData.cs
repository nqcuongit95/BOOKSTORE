using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreData
    {
        IEnumerable<KhachHang> GetAllKhachHang();

        void CreateCustomer(KhachHang customer);

        IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang();

        string GetTenLoaiKhachHang(int id);

    }
}
