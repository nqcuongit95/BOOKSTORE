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

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["QueryName"] = nameof(searchString);
            ViewData["SortDirection"] = "up";
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

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

            return View(await customers.AsNoTracking().ToListAsync());
        }

        public IActionResult CreateCustomer()
        {
            var loaiKhachHang = _bookStoreData.GetAllLoaiKhachHang();
            var model = new KhachHangViewModel();
            model.LoaiKhachHang = new SelectList(loaiKhachHang, "Id", "TenLoaiKhachHang",3);
             
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(KhachHangViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.KhachHang.NgayLap = DateTime.Now;
                _bookStoreData.CreateCustomer(model.KhachHang);

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
