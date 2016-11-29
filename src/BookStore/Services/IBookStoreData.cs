using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreData
    {
        IEnumerable<KhachHang> GetAllCustomer();

        void CreateCustomer(KhachHang customer);

        IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang();

    }
}
