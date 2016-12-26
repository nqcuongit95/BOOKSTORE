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
        Task<ProviderFilterResults> FindProvider(string value);
        Task<ProductFilterResults> FindProduct(string val);
        Task<ProductPriceViewModel> GetPrice(int id, int type);
        Task<bool> AddInvoice(InvoiceViewModel invoice,
            List<ProductBuyingDetailsViewModel> productDetails);        
        IQueryable<DonHang> GetAllDonHang();
        IQueryable<PhieuThuViewModel> GetAllPhieuThu();
        IQueryable<PhieuChiViewModel> GetAllPhieuChi();
        IEnumerable<LoaiPhieu> GetAllLoaiPhieu();
        IEnumerable<PhieuTraNhapHang> GetAllPhieuTraNhapHang();
        int TaoDonHang(DonHang donhang);
        void TaoPhieuThu(PhieuThu phieuthu);
        int TaoPhieuChi(PhieuChi phieuchi);
        void UpdateDonHang(int? id);
        int findDonHangByCustomer(int? customerID);
        int findUserId(String name);
        int findPhieuTraNhapHang(int? providerID);
        PhieuThuViewModel findPhieuThu(int phieuID);
        KhachHang findCustomerByDonhang(int donhangId);
        NhaCungCap findProviderByPhieuTra(int phieuID);
    }
}
