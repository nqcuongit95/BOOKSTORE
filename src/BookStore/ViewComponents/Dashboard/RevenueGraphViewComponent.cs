using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents.Dashboard
{
    public class RevenueGraphViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public RevenueGraphViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public IViewComponentResult Invoke()
        {            
            return View();
        }

    }
}
