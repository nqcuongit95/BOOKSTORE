using BookStore.Entities;
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
        void Commit();
        IQueryable<CustomerInfoViewModel> GetAllKhachHang();
        int CreateCustomer(KhachHang customer);
        IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang();
        string GetTenLoaiKhachHang(int id);
        CustomerInfoViewModel GetKhachHangInfo(int id);
        KhachHang GetKhachHang(int id);
        Task<CustomerTransactionsViewModel> GetCustomerTransactionsDetails(int id);
        Task<StatisticsViewModel> GetStatisticsInformation();
        Task<List<Role>> GetListRoles();
        Task<List<Staff>> GetListStaffs();
        Task<CustomerFilterResults> FindCustomer(string value);
        Task<ProductFilterResults> FindProduct(string val);
        Task<ProductPriceViewModel> GetPrice(int id, int type);
    }
}
