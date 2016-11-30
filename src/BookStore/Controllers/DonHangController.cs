using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class DonHangController : Controller
    {
        private IBookStoreData _bookStoreData;
        // GET: /<controller>/

        public DonHangController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        public IActionResult Index()
        {
            var model = _bookStoreData.GetAllDonHang();
            //ViewBag.listdh = _bookStoreData.GetAllDonHang();
            return View(model);
        }
    }
}
