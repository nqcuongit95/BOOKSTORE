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
    [Route("HangHoa/NhaCungCap")]
    public class NhaCungCapController : Controller
    {
        private readonly IBookStoreData _bookStoreData = null;
        private readonly IStringLocalizer<NhaCungCapController> _localizer;

        public NhaCungCapController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            try
            {
                if (page < 1)
                    page = 1;

                var result = await PaginatedList<NhaCungCap>.CreateAsync(
                    _bookStoreData.GetAllNhaCungCap(), page ?? 1, pageSize ?? 10);

                return View(result);
            }
            catch (Exception) { return NotFound(); }
        }
    }
}
