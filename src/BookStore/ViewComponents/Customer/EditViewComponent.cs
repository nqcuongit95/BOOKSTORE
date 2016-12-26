using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents
{
    public class EditViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;
        public EditViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public IViewComponentResult Invoke(int id)
        {
            var customer = _bookStoreData.GetKhachHang(id);
            var customerTypes = _bookStoreData.GetAllLoaiKhachHang();

            var model = new KhachHangViewModel();

            model.KhachHang = customer;
            model.LoaiKhachHang = new SelectList(customerTypes, "Id", "TenLoaiKhachHang", customer.LoaiKhachHangId);
            
            return View(model);
        }
    }
}
