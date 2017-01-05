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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class DonHangController : Controller
    {
        // GET: /<controller>/
        private IBookStoreData _bookStoreData;
        private UserManager<Staff> _usermanager;

        public DonHangController(IBookStoreData bookStoreData, UserManager<Staff> userManager)
        {
            _usermanager = userManager;
            _bookStoreData = bookStoreData;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString,
                                               string currentFilter, int? page,
                                               int? firstShowedPage, int? lastShowedPage)
        {

            //User.Identity.u
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

            var donhang = _bookStoreData.GetAllDonHang();

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    donhang = donhang.Where(c => c.TenKhachHang.Contains(searchString));
            //}

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
        public IActionResult Details(int id)
        {
            var donhang = _bookStoreData.findDonHangById(id);
            donhang.phieutraID = _bookStoreData.findPhieuTraByDonHang(donhang.ID);
            donhang.khachhang = _bookStoreData.findCustomerById(donhang.KhachHangId);
            donhang.details = _bookStoreData.GetCTDonHang(donhang.ID).ToList();
            donhang.listPhieuThu = _bookStoreData.findPhieuThuByDonHang(id).ToList();
            for (int i = 0; i < donhang.details.Count; i++)
            {
                donhang.details[i].ThanhTien = donhang.details[i].GiaBan * donhang.details[i].SoLuong;
            }
            decimal tienDaThu = 0;
            for (int i = 0; i < donhang.listPhieuThu.Count; i++)
            {
                tienDaThu += donhang.listPhieuThu[i].TongTien;
            }
            donhang.TienDaThu = tienDaThu;
            donhang.TienDaThuFormated = FormatDecimalValue(tienDaThu);
            return View(donhang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDonHang(decimal TienThu, int ID)
        {
            //try
            //{
                var phieuthu = new PhieuThu();
                phieuthu.DonHangId = ID;
                phieuthu.NgayLap = DateTime.Now;
                var user = await _usermanager.FindByNameAsync(User.Identity.Name);
                phieuthu.NhanVienId = user.Id;
                phieuthu.TongTien = TienThu;
                phieuthu.LoaiPhieuId = 2;
                var kh = _bookStoreData.findCustomerByDonhang((int)phieuthu.DonHangId);
                phieuthu.KhachHangId = kh.Id;
                _bookStoreData.TaoPhieuThu(phieuthu);
                var listPhieuThu = _bookStoreData.findPhieuThuByDonHang(ID).ToList();
                decimal tienDaThu = 0; ;
                for (int i = 0; i < listPhieuThu.Count; i++)
                {
                    tienDaThu += listPhieuThu[i].TongTien;
                }
                _bookStoreData.CapnhatDonhang(ID, tienDaThu);
                var noti = new Notification
                {
                    Title = "Thành Công",
                    Content = "Cập nhật đơn hàng thành công",
                    Icon = "checkmark",
                    MessageType = "positive",
                };
                return PartialView("_Notify", noti);
            //}
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
        //helper function
        public string FormatDecimalValue(decimal val)
        {
            return val.ToString("N0") + " đ";
        }
    }
}
