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
    public class CustomerGraphViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public CustomerGraphViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = _bookStoreData.GetAllKhachHang();

            var selectResult = from customer in query
                               group customer by customer.NgayLap.Month
                    into groupCustomer
                               select new NumberOfCustomersByMonthViewModel
                               {
                                   Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupCustomer.Key),
                                   Total = groupCustomer.Count()
                               };

            var model = await selectResult.ToListAsync();
            
            return View("Default", model);

        }
    }
}
