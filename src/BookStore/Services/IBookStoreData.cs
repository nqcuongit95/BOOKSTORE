using BookStore.Entities;
using BookStore.Helper;
using BookStore.Models;
using BookStore.ViewModels;
using BookStore.ViewModels.Customer;
using BookStore.ViewModels.Dashboard;
using BookStore.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreData
    {
        #region TrangThai
        IQueryable<TrangThai> GetTrangThai(string vietTat, string loai);
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
        IQueryable<HangHoa> GetHangHoa(string search, bool use);
        bool HangHoaExists(int? id);
        Task<int> AddHangHoa(HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties);
        Task<HangHoa> GetHangHoaById(int? id);
        Task<int> UpdateHangHoa(HangHoa hangHoa,
           ICollection<ChiTietHangHoa> properties);
        Task<int> DeleteHangHoa(int id);
        #endregion

        #region PhieuNhapHang
        IQueryable<PhieuNhapHang> GetPhieuNhapHang(string trangThai);
        bool PhieuNhapHangExists(int? id);
        Task<int> AddPhieuNhapHang(PhieuNhapHang phieuNhapHang,
            ICollection<ChiTietPhieuNhapHang> properties);
        Task<PhieuNhapHang> GetPhieuNhapHangById(int? id);
        Task<int> UpdatePhieuNhapHang(PhieuNhapHang phieuNhapHang,
           ICollection<ChiTietPhieuNhapHang> properties);
        Task<int> DeletePhieuNhapHang(int id);
        IQueryable<PhieuNhapHang> GetUnrecivedPhieuNhapHang();
        Task<int> PayPhieuNhapHang(int id);
        Task<int> RecivePhieuNhapHang(int id);
        #endregion

        #region ChiTietPhieuNhapHang
        IQueryable<ChiTietPhieuNhapHang> GetChiTietPhieuNhapHang(int? phieuNhapHangId);
        bool ChiTietPhieuNhapHangExists(int? id);
        Task<int> AddChiTietPhieuNhapHang(ChiTietPhieuNhapHang chiTietPhieuNhapHang);
        Task<ChiTietPhieuNhapHang> GetChiTietPhieuNhapHangById(int? id);
        Task<int> UpdateChiTietPhieuNhapHang(ChiTietPhieuNhapHang chiTietPhieuNhapHang);
        Task<int> DeleteChiTietPhieuNhapHang(int id);
        #endregion

        #region PhieuKiemKho
        IQueryable<PhieuKiemKho> GetPhieuKiemKho();
        bool PhieuKiemKhoExists(int? id);
        Task<int> AddPhieuKiemKho(PhieuKiemKho phieuKiemKho,
            ICollection<ChiTietPhieuKiemKho> properties);
        Task<PhieuKiemKho> GetPhieuKiemKhoById(int? id);
        Task<int> UpdatePhieuKiemKho(PhieuKiemKho phieuKiemKho,
           ICollection<ChiTietPhieuKiemKho> properties);
        Task<int> DeletePhieuKiemKho(int id);
        #endregion

        #region ChiTietPhieuKiemKho
        IQueryable<ChiTietPhieuKiemKho> GetChiTietPhieuKiemKho(int? phieuKiemKhoId);
        bool ChiTietPhieuKiemKhoExists(int? id);
        Task<int> AddChiTietPhieuKiemKho(ChiTietPhieuKiemKho chiTietPhieuKiemKho);
        Task<ChiTietPhieuKiemKho> GetChiTietPhieuKiemKhoById(int? id);
        Task<int> UpdateChiTietPhieuKiemKho(ChiTietPhieuKiemKho chiTietPhieuKiemKho);
        Task<int> DeleteChiTietPhieuKiemKho(int id);
        #endregion

        #region PhieuNhanHang
        IQueryable<PhieuNhanHang> GetPhieuNhanHang();
        bool PhieuNhanHangExists(int? id);
        Task<int> AddPhieuNhanHang(PhieuNhanHang phieuNhanHang);
        Task<PhieuNhanHang> GetPhieuNhanHangById(int? id);
        Task<int> UpdatePhieuNhanHang(PhieuNhanHang phieuNhanHang);
        Task<int> DeletePhieuNhanHang(int id);
        #endregion

        #region PhieuChiNhapHang
        IQueryable<PhieuChi> GetPhieuChiNhapHang();
        bool PhieuChiNhapHangExists(int? id);
        Task<int> AddPhieuChiNhapHang(PhieuChi phieuChi);
        Task<PhieuChi> GetPhieuChiNhapHangById(int? id);
        Task<int> UpdatePhieuChiNhapHang(PhieuChi phieuChi);
        Task<int> DeletePhieuChiNhapHang(int id);
        #endregion

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
        IQueryable<Staff> GetListStaffs();
        Task<CustomerFilterResults> FindCustomer(string value);
        Task<ProductFilterResults> FindProduct(string val);
        Task<ProductPriceViewModel> GetPrice(int id, int type);
        Task<Tuple<bool, int>> AddInvoice(InvoiceViewModel invoice,
            List<ProductBuyingDetailsViewModel> productDetails);       
        Task<List<ProductFilterViewModel>> GetBestSellingGoods(int take, TimeEnum time, ProductType type);
        Task<CustomerFilterViewModel> GetCustomerById(int id);
        Task<List<RevenueViewModel>> GetRevenueStatistics(int take, TimeEnum time);
        Task<List<FeedsViewModel>> GetFeeds(int take);
        Task<TransactionStatisticsViewModel> GetTransactionStatistics();
        Task<CustomerStatisticsViewModel> GetCustomerStatistics();
        Task<ProductStatisticViewModel> GetProductStatistics();
        Task<List<NumberOfCustomersByMonthViewModel>> GetCustomerRegisterStatistics();
        Task<CustomerLiabilitesViewModel> GetCustomerLiabilites(int id);
        Task<Staff> GetStaffByUserName(string userName);
        Task<BillViewModel> GetBill(int id);
    }
}
