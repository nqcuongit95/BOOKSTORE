using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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

        public IActionResult Index()
        {
            var model = _bookStoreData.GetAllCustomer();

            return View(model);
        }

        public IActionResult CreateCustomer()
        {
            var loaiKhachHang = _bookStoreData.GetAllLoaiKhachHang();
            var model = new KhachHangViewModel();
            model.LoaiKhachHang = new SelectList(loaiKhachHang, "Id", "TenLoaiKhachHang");
             
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
