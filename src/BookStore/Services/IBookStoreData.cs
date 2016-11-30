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

        List<object> GetAllDonHang();
        IEnumerable<DonHang> GetDonHang();
    }
}
