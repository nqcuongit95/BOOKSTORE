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
                                               string currentFilter,int? page,                                               
                                               int? firstShowedPage, int ? lastShowedPage)
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
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.NgayLap);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.NgayLap);
                    break;
                default:
                    customers = customers.OrderBy(c => c.TenKhachHang);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 9;
            int numberOfDisplayPages = 5;

            return View(await PaginatedList<CustomerInfoViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage,lastShowedPage));
        }

        public IActionResult Create()
        {
            var loaiKhachHang = _bookStoreData.GetAllLoaiKhachHang();
            var model = new KhachHangViewModel();
            model.LoaiKhachHang = new SelectList(loaiKhachHang, "Id", "TenLoaiKhachHang",3);
           
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
                
                return RedirectToAction("Details",new { id = ID, message = "_CreateMessage" });
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
                Section = string.IsNullOrEmpty(section) ? "Details" : section
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, KhachHangViewModel model)
        {
            var customer = _bookStoreData.GetKhachHang(id);

            if (ModelState.IsValid)
            {
                customer.TenKhachHang = model.KhachHang.TenKhachHang;
                customer.SoDienThoai = model.KhachHang.SoDienThoai;
                customer.Email = model.KhachHang.Email;
                customer.DiaChi = model.KhachHang.DiaChi;
                customer.LoaiKhachHangId = model.KhachHang.LoaiKhachHangId;

                _bookStoreData.Commit();

                return RedirectToAction("Details",new { id = id, message = "_UpdateMessage" });
                }

            return View("Details", new { id = id, section = "Edit" });            
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
    }
}
