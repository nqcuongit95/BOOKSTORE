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

        public async Task<IActionResult> CustomerRegisterGraph(int when = 3)
        {
            var timeline = (Helper.TimeEnum)when;
            var model = await _bookStoreData.GetCustomerRegisterStatistics();  

            return PartialView("_CustomerRegisterGraph", model);

        }
        public async Task<IActionResult> BestSellingGoodsGraph(int when = 3)
        {
            var timeline = (Helper.TimeEnum)when;

            var model = await _bookStoreData.GetBestSellingGoods(6, timeline, Helper.ProductType.Both);

            return PartialView("_BestSellingGoodsGraph", model);

        }
        public async Task<IActionResult> LastestActivity()
        {
            var model = await _bookStoreData.GetFeeds(8);
            return PartialView("_LastestActivity", model);
        }

        public async Task<IActionResult> RevenueStatistic(int when = 2)
        {
            var timeline = (Helper.TimeEnum)when;
            var model = await _bookStoreData.GetRevenueStatistics(5, timeline);
            return PartialView("_RevenueStatisticsGraph", model);
        }
        
    }
}
