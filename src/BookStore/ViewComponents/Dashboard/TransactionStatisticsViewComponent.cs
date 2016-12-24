using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents.Dashboard
{
    public class TransactionStatisticsViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public TransactionStatisticsViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _bookStoreData.GetTransactionStatistics();

            return View("Default",model);
        }
    }
}
