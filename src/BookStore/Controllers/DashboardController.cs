using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using BookStore.ViewModels;
using System.Globalization;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private IBookStoreData _bookStoreData;

        public DashboardController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CustomerRegisterGraph()
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

            return PartialView("_CustomerRegisterGraph", model);

        }

        public async Task<IActionResult> BestSellingGoodsGraph()
        {
            var model = await _bookStoreData.GetBestSellingGoods(6, Helper.TimeEnum.Week,Helper.ProductType.Both);

            return PartialView("_BestSellingGoodsGraph", model);

        }
        public async Task<IActionResult> LastestActivity()
        {
            var model = await _bookStoreData.GetFeeds(8);
            return PartialView("_LastestActivity",model);
        }

        public async Task<IActionResult> RevenueStatistic()
        {
            var model = await _bookStoreData.GetRevenueStatistics(5, Helper.TimeEnum.Week);
            return PartialView("_RevenueStatisticsGraph", model);
        }

    }
}
