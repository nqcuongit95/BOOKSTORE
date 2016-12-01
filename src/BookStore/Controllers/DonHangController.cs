using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class DonHangController : Controller
    {
        private IBookStoreData _bookStoreData;
        readonly BOOKSTOREContext _context = new BOOKSTOREContext();
        // GET: /<controller>/

        //public DonHangController(IBookStoreData bookStoreData)
        //{
        //    _bookStoreData = bookStoreData;
        //}

        public DonHangController()
        {
        }

        public IActionResult Index()
        {
            var model = _bookStoreData.GetDonHang();
            //var viewmodel = new DonHangViewModels();
            var viewmodel = _context.DonHang
                .Include(i => i.KhachHang);
                //.ToListAsync();
            //ViewBag.listdh = _bookStoreData.GetAllDonHang();
            return View(viewmodel);
        }
    }
}
