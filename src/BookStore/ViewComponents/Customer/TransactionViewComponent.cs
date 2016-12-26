using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents
{
    public class TransactionViewComponent : ViewComponent
    {
        private IBookStoreData _bookStoreData;

        public TransactionViewComponent(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await _bookStoreData.GetCustomerTransactionsDetails(id);

            return View(model);
        }
    }
}
