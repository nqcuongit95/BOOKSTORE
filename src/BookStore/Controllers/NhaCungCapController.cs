using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using Microsoft.Extensions.Localization;
using BookStore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly IBookStoreData _bookStoreData = null;

        public NhaCungCapController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        [HttpGet]
        [Route("HangHoa/NhaCungCap")]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            try
            {
                var result = await PaginatedList<NhaCungCap>.CreateAsync(
                    _bookStoreData.GetAllNhaCungCap(), page ?? 1, pageSize ?? 10);

                return View(result);
            }
            catch (Exception) { return NotFound(); }
        }

        [HttpGet]
        [Route("HangHoa/NhaCungCap/Create")]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,NgayLap,TenNhaCungCap")] NhaCungCap nhaCungCap)
        //{
        //    if (ModelState.IsValid)
        //    {
        //    }
        //    return View(nhaCungCap);
        //}
    }
}
