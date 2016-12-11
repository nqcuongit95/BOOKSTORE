using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class SaleController : Controller
    {
        private IBookStoreData _bookStoreData;

        public SaleController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public IActionResult Index()
        {
            return View();
        }

        //find customers
        public async Task<IActionResult> FindCustomer(string val)
        {
            var result =await _bookStoreData.FindCustomer(val);

            return Json(result);    
        }
    }
}
