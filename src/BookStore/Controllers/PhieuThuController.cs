using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.ViewModels;
using BookStore.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class PhieuThuController : Controller
    {
        private IBookStoreData _bookStoreData;
        public PhieuThuController(IBookStoreData bookStoreData)
        {
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

            var customers = _bookStoreData.GetAllPhieuThu();

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.ID);
                    ViewData["SortDirection"] = "down";
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.NgayLap);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.NgayLap);
                    break;
                default:
                    customers = customers.OrderByDescending(c => c.NgayLap);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 8;
            int numberOfDisplayPages = 5;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    customers = customers.Where(c => c.TenLoaiPhieu.Contains(searchString));
            //}

            var result = await PaginatedList<PhieuThuViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].DonHangId.HasValue || result[i].KhachHangId.HasValue)
                {
                    result[i].DoiTuong = "Khách hàng";
                }
                if (result[i].NCCId.HasValue)
                {
                    result[i].DoiTuong = "Nhà cung cấp";
                }
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.ID.ToString().Contains(searchString));
                return View(await PaginatedList<PhieuThuViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
            else
            {
                return View(result);
            }

        }


        public IActionResult Create()
        {
            var loaiphieu = _bookStoreData.GetAllLoaiPhieuThu();
            var model = new PhieuThuViewModel();
            model.LoaiPhieu = new SelectList(loaiphieu, "Id", "TenLoaiPhieu", 1);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(decimal TongTien, int? KhachHangId, int? NCCId, int LoaiPhieuId)
        {
            if (ModelState.IsValid)
            {
                PhieuThu phieu = new PhieuThu();
                phieu.NgayLap = DateTime.Now;
                phieu.NhanVienId = _bookStoreData.findUserId(User.Identity.Name);
                phieu.TongTien = TongTien;
                phieu.LoaiPhieuId = LoaiPhieuId;
                if (KhachHangId != null)
                {
                    phieu.KhachHangId = KhachHangId;
                    _bookStoreData.TaoPhieuThu(phieu);
                    return RedirectToAction("Index");
                }
                if (KhachHangId == null && NCCId == null)
                {
                    phieu.KhachHangId = 1;
                    _bookStoreData.TaoPhieuThu(phieu);
                    return RedirectToAction("Index");
                }
                if (NCCId != null)
                {
                    phieu.NhaCungCapId = NCCId;
                    _bookStoreData.TaoPhieuThu(phieu);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public async Task<IActionResult> FindCustomer(string val)
        {
            var result = await _bookStoreData.FindCustomer(val);

            return Json(result);
        }
        public async Task<IActionResult> FindProvider(string val)
        {
            var result = await _bookStoreData.FindProvider(val);

            return Json(result);
        }

        public IActionResult Details(int id)
        {
            var phieu = _bookStoreData.findPhieuThu(id);

            if (phieu.DonHangId.HasValue)
            {
                var khachhang = _bookStoreData.findCustomerByDonhang((int)phieu.DonHangId);
                phieu.DoiTuong = "Khách Hàng";
                phieu.TenKhachHang = khachhang.TenKhachHang;
            }
            if (phieu.KhachHangId.HasValue)
            {
                var khach = _bookStoreData.findCustomerById((int)phieu.KhachHangId);
                phieu.DoiTuong = "Khách Hàng";
                phieu.TenKhachHang = khach.TenKhachHang;
            }
            if (phieu.NCCId.HasValue)
            {
                var ncc = _bookStoreData.findProviderById((int)phieu.NCCId);
                phieu.TenNhaCungCap = ncc.TenNhaCungCap;
                phieu.DoiTuong = "Nhà Cung Cấp";
            }
            return View(phieu);
        }
    }
}

