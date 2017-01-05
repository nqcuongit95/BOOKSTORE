using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.ViewModels;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using BookStore.Entities;

namespace BookStore.Controllers
{
    public class TraHangController : Controller
    {
        // GET: /<controller>/
        private IBookStoreData _bookStoreData;
        private UserManager<Staff> _usermanager;
        public TraHangController(IBookStoreData bookStoreData, UserManager<Staff> userManager)
        {
            _usermanager = userManager;
            _bookStoreData = bookStoreData;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString,
                                               string currentFilter, int? page,
                                               int? firstShowedPage, int? lastShowedPage)
        {
            ViewData["QueryName"] = nameof(searchString);
            ViewData["SortDirection"] = "up";

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var trahang = _bookStoreData.GetAllPhieuTraHang();

            switch (sortOrder)
            {
                case "name_desc":
                    trahang = trahang.OrderByDescending(c => c.ID);
                    ViewData["SortDirection"] = "down";
                    break;
                case "Date":
                    trahang = trahang.OrderBy(c => c.NgayLap);
                    break;
                case "date_desc":
                    trahang = trahang.OrderByDescending(c => c.NgayLap);
                    break;
                default:
                    trahang = trahang.OrderByDescending(c => c.NgayLap);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 9;
            int numberOfDisplayPages = 5;


            if (!string.IsNullOrEmpty(searchString))
            {
                trahang = from p in trahang
                          where p.ID.ToString().Contains(searchString)
                          || p.DonHangId.ToString().Contains(searchString)
                          select p;
                return View(await PaginatedList<TraHangViewModel>.
                        CreateAsync(trahang, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
            else
            {
                return View(await PaginatedList<TraHangViewModel>.
                        CreateAsync(trahang, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
        }
        public async Task<IActionResult> ListDonHang(string sortOrder, string searchString,
                                              string currentFilter, int? page,
                                              int? firstShowedPage, int? lastShowedPage)
        {
            ViewData["QueryName"] = nameof(searchString);
            ViewData["SortDirection"] = "up";

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "na" ? "na_desc" : "na";
            ViewData["MoneySortParm"] = sortOrder == "money" ? "money_desc" : "money";
            ViewData["StatusSortParm"] = sortOrder == "status" ? "status_desc" : "status";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var donhang = _bookStoreData.GetDonHangWithOutPtra();

            if (!string.IsNullOrEmpty(searchString))
            {
                donhang = donhang.Where(c => c.Id.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    donhang = donhang.OrderByDescending(c => c.Id);
                    ViewData["SortDirection"] = "down";
                    break;
                case "na_desc":
                    donhang = donhang.OrderByDescending(c => c.KhachHang.TenKhachHang);
                    //ViewData["SortDirection"] = "down";
                    break;
                case "na":
                    donhang = donhang.OrderBy(c => c.KhachHang.TenKhachHang);
                    break;
                case "money":
                    donhang = donhang.OrderBy(c => c.TongTien);
                    break;
                case "money_desc":
                    donhang = donhang.OrderByDescending(c => c.TongTien);
                    break;
                case "status":
                    donhang = donhang.OrderBy(c => c.TrangThai.TenTrangThai);
                    break;
                case "status_desc":
                    donhang = donhang.OrderByDescending(c => c.TrangThai.TenTrangThai);
                    break;
                case "Date":
                    donhang = donhang.OrderBy(c => c.NgayLap);
                    break;
                case "date_desc":
                    donhang = donhang.OrderByDescending(c => c.NgayLap);
                    break;
                default:
                    donhang = donhang.OrderByDescending(c => c.NgayLap);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 9;
            int numberOfDisplayPages = 5;
           

            if (!string.IsNullOrEmpty(searchString))
            {
                donhang = donhang.Where(c => c.Id.ToString().Contains(searchString));
                return View(await PaginatedList<DonHang>.
                        CreateAsync(donhang, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
            else
            {
                return View(await PaginatedList<DonHang>.
                        CreateAsync(donhang, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
        }

        public IActionResult Create(int id)
        {
            // if ()
            var donhang = _bookStoreData.findDonHangById(id);
            donhang.khachhang = _bookStoreData.findCustomerById(donhang.KhachHangId);
            donhang.details = _bookStoreData.GetCTDonHang(donhang.ID).ToList();
            donhang.listPhieuThu = _bookStoreData.findPhieuThuByDonHang(id).ToList();
            for (int i = 0; i < donhang.details.Count; i++)
            {
                donhang.details[i].ThanhTien = donhang.details[i].GiaBan * donhang.details[i].SoLuong;
            }
            for (int i = 0; i < donhang.listPhieuThu.Count; i++)
            {
                donhang.TienDaThu += donhang.listPhieuThu[i].TongTien;
            }
            //var donhang = new DonHangViewModel();
            return View(donhang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal TienThu, int ID, List<ChiTietTraHang> returnDetails)
        {
            //try
            {
                var phieutra = new PhieuTraHang();
                phieutra.DonHangId = ID;
                phieutra.NgayLap = DateTime.Now;
                var user = await _usermanager.FindByNameAsync(User.Identity.Name);
                phieutra.NhanVienId = user.Id;
                phieutra.TongTien = TienThu;
                var kh = _bookStoreData.findCustomerByDonhang((int)phieutra.DonHangId);
                phieutra.KhachHangId = kh.Id;
                _bookStoreData.TaoPhieuTraHang(phieutra);
                var phieuchi = new PhieuChi();
                var pt = _bookStoreData.findNewPhieuTraHang();
                phieuchi.NhanVienId = user.Id;
                phieuchi.PhieuTraHangId = pt.Id;
                phieuchi.TongTien = phieutra.TongTien;
                phieuchi.LoaiPhieuId = 10;
                phieuchi.NgayLap = DateTime.Now;
                _bookStoreData.TaoPhieuChi(phieuchi);
                
                //List<ChiTietPhieuTraHang> chitietphieu = new List<ChiTietPhieuTraHang>();
                for (int i = 0; i < returnDetails.Count; i++)
                {
                    var chitiet = new ChiTietPhieuTraHang();
                    chitiet.PhieuTraHangId = pt.Id;
                    chitiet.ChiTietDonHangId = returnDetails[i].ChiTietDonHangId;
                    chitiet.SoLuong = returnDetails[i].Soluong;
                    chitiet.GiaTra = returnDetails[i].GiaTra;
                    _bookStoreData.TaoCTPhieuTraHang(chitiet);
                }
                var noti = new Notification
                {
                    Title = "Thành Công",
                    Content = "Tạo phiếu trả hàng thành công",
                    Icon = "checkmark",
                    MessageType = "positive",
                };
                return PartialView("_Notify", noti);
            }
            //catch (Exception e)
            //{
            //    var noti = new Notification
            //    {
            //        Title = "Thất bại",
            //        Content = "Có lỗi xảy ra" + e.Message,
            //        Icon = "remove",
            //        MessageType = "negative",
            //    };
            //    return PartialView("_Notify", noti);
            //}
        }

        public IActionResult Details(int id)
        {
            var phieutra = _bookStoreData.findPhieuTra(id);
            phieutra.DonHang = _bookStoreData.findDonHangById(phieutra.DonHangId);
            phieutra.DonHang.listPhieuThu = _bookStoreData.findPhieuThuByDonHang(id).ToList();
            for (int i = 0; i < phieutra.DonHang.listPhieuThu.Count; i++)
            {
                phieutra.DonHang.TienDaThu += phieutra.DonHang.listPhieuThu[i].TongTien;
            }
            phieutra.details = _bookStoreData.GetCTTraHang(phieutra.ID).ToList();
            for (int i = 0; i < phieutra.details.Count; i++)
            {
                phieutra.details[i].ThanhTien = phieutra.details[i].GiaTra * phieutra.details[i].SoLuong;
            }
            return View(phieutra);
        }

        public string FormatDecimalValue(decimal val)
        {
            return val.ToString("N0") + " đ";
        }
    }
}
