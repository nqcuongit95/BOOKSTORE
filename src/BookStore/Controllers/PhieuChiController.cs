using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.ViewModels;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class PhieuChiController : Controller
    {
        private IBookStoreData _bookStoreData;
        public PhieuChiController(IBookStoreData bookStoreData)
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

            var customers = _bookStoreData.GetAllPhieuChi();

           

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

            int pageSize = 9;
            int numberOfDisplayPages = 5;

            var result = await PaginatedList<PhieuChiViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].PhieuTraHangId.HasValue)
                {
                    result[i].DoiTuong = "Khách hàng";
                }
                else
                {
                    result[i].DoiTuong = "Nhà cung cấp";
                }
            }
             if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.ID.ToString().Contains(searchString));
                return View(await PaginatedList<PhieuChiViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
            else
            {
                return View(result);
            }
            
        }

        public IActionResult Details(int id)
        {
            var phieu = _bookStoreData.findPhieuChi(id);

            if (phieu.PhieuTraHangId.HasValue)
            {
                var khachhang = _bookStoreData.findCustomerByPhieuTra((int)phieu.PhieuTraHangId);
                phieu.DoiTuong = "Khách Hàng";
                phieu.TenKhachHang = khachhang.TenKhachHang;
            }
            else
            {
                var ncc = _bookStoreData.findProviderByPhieuNhap((int)phieu.PhieuNhapHangId);
                phieu.TenNhaCungCap = ncc.TenNhaCungCap;
                phieu.DoiTuong = "Nhà Cung Cấp";
            }
            return View(phieu);
        }

        public IActionResult Create()
        {
            var loaiphieu = _bookStoreData.GetAllLoaiPhieuChi();
            var model = new PhieuChiViewModel();
            model.LoaiPhieu = new SelectList(loaiphieu, "Id", "TenLoaiPhieu", 1);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhieuChiViewModel model)
        {
            if (ModelState.IsValid)
            {
                PhieuChi phieu = new PhieuChi();
                phieu.NgayLap = DateTime.Now;
                phieu.NhanVienId = _bookStoreData.findUserId(User.Identity.Name);
                phieu.TongTien = model.TongTien;
                phieu.LoaiPhieuId = model.LoaiPhieuId;

                if (model.KhachHangId != null)
                {
                    phieu.PhieuTraHangId = _bookStoreData.findPhieuTraByCustomer(model.KhachHangId);
                    _bookStoreData.TaoPhieuChi(phieu);
                    return RedirectToAction("Index");
                }
                if (model.NCCId != null)
                {
                    phieu.PhieuNhapHangId = _bookStoreData.findPhieuNhapByCustomer(model.NCCId);
                    _bookStoreData.TaoPhieuChi(phieu);
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
    }
}
