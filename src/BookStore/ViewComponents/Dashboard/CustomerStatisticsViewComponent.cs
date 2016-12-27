using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookStore.ViewComponents.Dashboard
{
    public class CustomerStatisticsViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public CustomerStatisticsViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _bookStoreData.GetCustomerStatistics();
            
            return View("Default",model);

        }
    }
}
