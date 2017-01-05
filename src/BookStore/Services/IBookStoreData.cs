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

        #endregion TrangThai

        #region NhaCungCap

        IQueryable<NhaCungCap> GetNhaCungCap();

        bool NhaCungCapExists(int? id);

        Task<int> AddNhaCungCap(NhaCungCap nhaCungCap);

        Task<NhaCungCap> GetNhaCungCapById(int? id);

        Task<int> UpdateNhaCungCap(NhaCungCap nhaCungCap);

        Task<int> DeleteNhaCungCap(int id);

        #endregion NhaCungCap

        #region NhanHieu

        IQueryable<NhanHieu> GetNhanHieu();

        bool NhanHieuExists(int? id);

        Task<int> AddNhanHieu(NhanHieu nhanHieu);

        Task<NhanHieu> GetNhanHieuById(int? id);

        Task<int> UpdateNhanHieu(NhanHieu nhanHieu);

        Task<int> DeleteNhanHieu(int id);

        #endregion NhanHieu

        #region LoaiHangHoa

        IQueryable<LoaiHangHoa> GetLoaiHangHoa();

        Task<LoaiHangHoa> GetLoaiHangHoaById(int? id);

        bool LoaiHangHoaExists(int? id);

        Task<int> AddLoaiHangHoa(LoaiHangHoa loaiHangHoa);

        Task<int> UpdateLoaiHangHoa(LoaiHangHoa loaiHangHoa);

        Task<int> DeleteLoaiHangHoa(int id);

        #endregion LoaiHangHoa

        #region ThuocTinhHangHoa

        IQueryable<ThuocTinhHangHoa> GetThuocTinhHangHoa(int? loaiHangHoaId);

        bool ThuocTinhHangHoaExists(int? id);

        Task<int> AddThuocTinhHangHoa(ThuocTinhHangHoa nhanHieu);

        Task<ThuocTinhHangHoa> GetThuocTinhHangHoaById(int? id);

        Task<int> UpdateThuocTinhHangHoa(ThuocTinhHangHoa nhanHieu);

        Task<int> DeleteThuocTinhHangHoa(int id);

        #endregion ThuocTinhHangHoa

        #region ChiTietHangHoa

        IQueryable<ChiTietHangHoa> GetChiTietHangHoa(int? hangHoaId);

        bool ChiTietHangHoaExists(int? id);

        Task<int> AddChiTietHangHoa(ChiTietHangHoa chiTietHangHoa);

        Task<ChiTietHangHoa> GetChiTietHangHoaById(int? id);

        Task<int> UpdateChiTietHangHoa(ChiTietHangHoa chiTietHangHoa);

        Task<int> DeleteChiTietHangHoa(int id);

        #endregion ChiTietHangHoa

        #region HangHoa

        IQueryable<HangHoa> GetHangHoa(string search, string state);

        bool HangHoaExists(int? id);

        Task<int> AddHangHoa(HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties);

        Task<HangHoa> GetHangHoaById(int? id);

        Task<int> UpdateHangHoa(HangHoa hangHoa,
           ICollection<ChiTietHangHoa> properties);

        Task<int> DeleteHangHoa(int id);

        #endregion HangHoa

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

        #endregion PhieuNhapHang

        #region ChiTietPhieuNhapHang

        IQueryable<ChiTietPhieuNhapHang> GetChiTietPhieuNhapHang(int? phieuNhapHangId);

        bool ChiTietPhieuNhapHangExists(int? id);

        Task<int> AddChiTietPhieuNhapHang(ChiTietPhieuNhapHang chiTietPhieuNhapHang);

        Task<ChiTietPhieuNhapHang> GetChiTietPhieuNhapHangById(int? id);

        Task<int> UpdateChiTietPhieuNhapHang(ChiTietPhieuNhapHang chiTietPhieuNhapHang);

        Task<int> DeleteChiTietPhieuNhapHang(int id);

        #endregion ChiTietPhieuNhapHang

        #region PhieuKiemKho

        IQueryable<PhieuKiemKho> GetPhieuKiemKho();

        bool PhieuKiemKhoExists(int? id);

        Task<int> AddPhieuKiemKho(PhieuKiemKho phieuKiemKho,
            ICollection<ChiTietPhieuKiemKho> properties);

        Task<PhieuKiemKho> GetPhieuKiemKhoById(int? id);

        Task<int> UpdatePhieuKiemKho(PhieuKiemKho phieuKiemKho,
           ICollection<ChiTietPhieuKiemKho> properties);

        Task<int> DeletePhieuKiemKho(int id);

        #endregion PhieuKiemKho

        #region ChiTietPhieuKiemKho

        IQueryable<ChiTietPhieuKiemKho> GetChiTietPhieuKiemKho(int? phieuKiemKhoId);

        bool ChiTietPhieuKiemKhoExists(int? id);

        Task<int> AddChiTietPhieuKiemKho(ChiTietPhieuKiemKho chiTietPhieuKiemKho);

        Task<ChiTietPhieuKiemKho> GetChiTietPhieuKiemKhoById(int? id);

        Task<int> UpdateChiTietPhieuKiemKho(ChiTietPhieuKiemKho chiTietPhieuKiemKho);

        Task<int> DeleteChiTietPhieuKiemKho(int id);

        #endregion ChiTietPhieuKiemKho

        #region PhieuNhanHang

        IQueryable<PhieuNhanHang> GetPhieuNhanHang();

        bool PhieuNhanHangExists(int? id);

        Task<int> AddPhieuNhanHang(PhieuNhanHang phieuNhanHang);

        Task<PhieuNhanHang> GetPhieuNhanHangById(int? id);

        Task<int> UpdatePhieuNhanHang(PhieuNhanHang phieuNhanHang);

        Task<int> DeletePhieuNhanHang(int id);

        #endregion PhieuNhanHang

        #region PhieuChiNhapHang

        IQueryable<PhieuChi> GetPhieuChiNhapHang();

        bool PhieuChiNhapHangExists(int? id);

        Task<int> AddPhieuChiNhapHang(PhieuChi phieuChi);

        Task<PhieuChi> GetPhieuChiNhapHangById(int? id);

        Task<int> UpdatePhieuChiNhapHang(PhieuChi phieuChi);

        Task<int> DeletePhieuChiNhapHang(int id);

        #endregion PhieuChiNhapHang

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
        IQueryable<Staff> GetListStaffQueryable();        

        Task<CustomerFilterResults> FindCustomer(string value);
        Task<ProviderFilterResults> FindProvider(string value);
        Task<ProductFilterResults> FindProduct(string val);

        Task<ProductPriceViewModel> GetPrice(int id, int type);

              
        IQueryable<DonHang> GetAllDonHang();
        IQueryable<DonHang> GetDonHangWithOutPtra();
        IQueryable<PhieuThuViewModel> GetAllPhieuThu();
        IQueryable<PhieuChiViewModel> GetAllPhieuChi();
        IQueryable<TraHangViewModel> GetAllPhieuTraHang();
        IQueryable<CTDonHang> GetCTDonHang(int donhangID);
        IQueryable<TraHangDetailViewModel> GetCTTraHang(int phieutraID);
        IEnumerable<LoaiPhieu> GetAllLoaiPhieuChi();
        IEnumerable<LoaiPhieu> GetAllLoaiPhieuThu();
        int findLoaiPhieuByLoai(string loai);
        int findTrangThaiByVietTat(string vt);
        IEnumerable<PhieuTraNhapHang> GetAllPhieuTraNhapHang();
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

        CustomerLiabilitesViewModel GetCustomerLiabilites(int id);
        Task<Staff> GetStaffByUserName(string userName);
        int TaoDonHang(DonHang donhang);
        void TaoPhieuThu(PhieuThu phieuthu);
        void TaoPhieuChi(PhieuChi phieuchi);
        void TaoPhieuTraHang(PhieuTraHang pth);
        void TaoCTPhieuTraHang(ChiTietPhieuTraHang pth);
        void UpdateDonHang(int? id);
        void CapnhatDonhang(int? id, decimal tienthu);
        int? findPhieuTraByDonHang(int donhangID);

        int findDonHangByCustomer(int? customerID);
        int findPhieuTraByCustomer(int? customerID);
        int findPhieuNhapByCustomer(int? customerID);
        int findUserId(String name);
        int findPhieuTraNhapHang(int? providerID);
        PhieuThuViewModel findPhieuThu(int phieuID);
        IQueryable<PhieuThu> findPhieuThuByDonHang(int donHangID);
        PhieuChiViewModel findPhieuChi(int phieuID);
        TraHangViewModel findPhieuTra(int phieuID);
        DonHangViewModel findDonHangById(int donhangID);
        KhachHang findCustomerByDonhang(int donhangId);
        CustomerInfoViewModel findCustomerById(int customerId);
        NhaCungCap findProviderByPhieuTra(int phieuID);
        NhaCungCap findProviderByPhieuNhap(int phieuID);
        NhaCungCap findProviderById(int ID);
        KhachHang findCustomerByPhieuTra(int donhangId);
        PhieuTraHang findNewPhieuTraHang();
        Task<BillViewModel> GetBill(int id);
    }
}