using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.ViewModels.Customer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class CustomerController : Controller
    {
        private IBookStoreData _bookStoreData;

        public CustomerController(IBookStoreData bookStoreData)
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

            var customers = _bookStoreData.GetAllKhachHang();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.TenKhachHang.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.TenKhachHang);
                    ViewData["SortDirection"] = "down";
                    ViewData["CurrentSortTHead"] = "0";
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.NgayLap);
                    ViewData["SortDirection"] = "up";
                    ViewData["CurrentSortTHead"] = "4";
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.NgayLap);
                    ViewData["SortDirection"] = "down";
                    ViewData["CurrentSortTHead"] = "4";
                    break;
                default:
                    customers = customers.OrderBy(c => c.TenKhachHang);
                    ViewData["SortDirection"] = "up";
                    ViewData["CurrentSortTHead"] = "0";
                    break;
            }

            int pageSize = 9;
            int numberOfDisplayPages = 5;

            return View(await PaginatedList<CustomerInfoViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
        }

        public IActionResult Create()
        {
            var loaiKhachHang = _bookStoreData.GetAllLoaiKhachHang();
            var model = new KhachHangViewModel();
            model.LoaiKhachHang = new SelectList(loaiKhachHang, "Id", "TenLoaiKhachHang", 3);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KhachHangViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.KhachHang.NgayLap = DateTime.Now;
                var ID = _bookStoreData.CreateCustomer(model.KhachHang);

                return RedirectToAction("Details", new { id = ID, message = "_CreateMessage" });
            }

            return View();
        }

        public IActionResult Details(int id, string section, string message)
        {
            //render the success message
            ViewData["message"] = message;

            ActiveItemHelperFunction(section);

            var customer = _bookStoreData.GetKhachHangInfo(id);

            var model = new CustomerDetailsViewModel
            {
                ID = customer.ID,
                Name = customer.TenKhachHang,
                Section = string.IsNullOrEmpty(section) ? "Details" :
                section
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, KhachHangViewModel model)
        {
            var customer = _bookStoreData.GetKhachHang(id);
            Notification notify;

            if (ModelState.IsValid)
            {
                customer.TenKhachHang = model.KhachHang.TenKhachHang;
                customer.SoDienThoai = model.KhachHang.SoDienThoai;
                customer.Email = model.KhachHang.Email;
                customer.DiaChi = model.KhachHang.DiaChi;
                customer.LoaiKhachHangId = model.KhachHang.LoaiKhachHangId;

                _bookStoreData.Commit();

                notify = new Notification
                {
                    Icon = "checkmark",
                    Title = "Thành Công",
                    Content = "Cập nhật thông tin khách hàng thành công",
                    MessageType = "positive",
                    Button = "Hoàn tất"

                };

                return PartialView("_Notify", notify);
            }

            notify = new Notification
            {
                Title = "Thất bại",
                Content = "Có lỗi xảy ra",
                Button = "Quay lại"
            };

            return PartialView("_Notify", notify);
        }

        void ActiveItemHelperFunction(string section)
        {
            ViewData["Details"] = "";
            ViewData["Edit"] = "";
            ViewData["Transaction"] = "";
            ViewData["Liabilities"] = "";

            if (string.IsNullOrEmpty(section))
            {
                ViewData["Details"] = "active";
            }
            else
            {
                ViewData[section] = "active";
            }
        }

        public IActionResult CustomerDetails(int id)
        {
            var model = _bookStoreData.GetKhachHangInfo(id);

            return PartialView("_CustomerDetails", model);
        }

        public IActionResult CustomerEdit(int id)
        {
            var customer = _bookStoreData.GetKhachHang(id);
            var customerTypes = _bookStoreData.GetAllLoaiKhachHang();

            var model = new KhachHangViewModel();

            model.KhachHang = customer;
            model.LoaiKhachHang = new SelectList(customerTypes, "Id", "TenLoaiKhachHang", customer.LoaiKhachHangId);

            return PartialView("_CustomerEdit", model);
        }

        public async Task<IActionResult> CustomerTransaction(int id, int? page,
                                               int? firstShowedPage, int? lastShowedPage)
        {
            var model = await _bookStoreData.GetCustomerTransactionsDetails(id);
            model.InvoicesPage = await PaginatedList<InvoiceDetailsViewModel>
                                 .CreateAsync(model.Invoices, page ?? 1, 5, 5, firstShowedPage, lastShowedPage);

            return PartialView("_CustomerTransaction", model);
        }

        public async Task<IActionResult> CustomerLiabilites(int id, int? page,
                                               int? firstShowedPage, int? lastShowedPage)
        {
            var model = _bookStoreData.GetCustomerLiabilites(id);
            model.CustomerId = id;


            decimal totalDebts = await model.sourceDebts.SumAsync(d => d.Value);
            model.TotalDebts = totalDebts;
            var list = await model.sourceDebts.OrderByDescending(m => m.DateCreate).ToListAsync();

            model.Debts = await PaginatedList<DebtViewModel>
                .CreateAsync(model.sourceDebts.OrderByDescending(i => i.DateCreate),
                page ?? 1, 7, 5,
                firstShowedPage,
                lastShowedPage);

            for (int i = 0; i < model.Debts.Count; i++)
            {
                var index = list.FindIndex(d => d.Id == model.Debts[i].Id);
                var currentDebt = list.Skip(index).Sum(m => m.Value);
                model.Debts[i].Debt = currentDebt;

            }

            return PartialView("_CustomerLiabilites", model);
        }
    }
}
