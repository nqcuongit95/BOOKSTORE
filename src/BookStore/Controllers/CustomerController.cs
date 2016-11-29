using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class CustomerController : Controller
    {
        private IBookStoreData _bookStoreData;

        public CustomerController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public IActionResult Index()
        {
            var model = _bookStoreData.GetAllCustomer();

            return View(model);
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(Khachhang model)
        {
            if (ModelState.IsValid)
            {
                _bookStoreData.CreateCustomer(model);
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
