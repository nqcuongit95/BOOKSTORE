using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents
{
    public class DetailsViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public DetailsViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }
        
        public IViewComponentResult Invoke(int id)
        {
            var model = _bookStoreData.GetKhachHangInfo(id);
            
            return View("Default", model);           
        }
    }
}
