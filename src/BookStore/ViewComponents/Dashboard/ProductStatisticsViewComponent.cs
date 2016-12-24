using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents.Dashboard
{
    public class ProductStatisticsViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public ProductStatisticsViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _bookStoreData.GetProductStatistics();

            return View("Default", model);
        }

    }
}
