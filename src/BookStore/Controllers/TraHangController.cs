using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class TraHangController : Controller
    {
        // GET: /<controller>/
        private IBookStoreData _bookStoreData;
        public TraHangController(IBookStoreData bookStoreData)
        {
            _bookStoreData = bookStoreData;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString,
                                               string currentFilter, int? page,
                                               int? firstShowedPage, int? lastShowedPage)
        {
            ViewData["QueryName"] = nameof(searchString);
            ViewData["SortDirection"] = "up";

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var trahang = _bookStoreData.GetAllPhieuTraHang();

            switch (sortOrder)
            {
                case "name_desc":
                    trahang = trahang.OrderByDescending(c => c.ID);
                    ViewData["SortDirection"] = "down";
                    break;
                case "Date":
                    trahang = trahang.OrderBy(c => c.NgayLap);
                    break;
                case "date_desc":
                    trahang = trahang.OrderByDescending(c => c.NgayLap);
                    break;
                default:
                    trahang = trahang.OrderBy(c => c.ID);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 9;
            int numberOfDisplayPages = 5;


            if (!string.IsNullOrEmpty(searchString))
            {
                trahang = trahang.Where(c => c.ID.ToString().Contains(searchString));
                return View(await PaginatedList<TraHangViewModel>.
                        CreateAsync(trahang, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }
            else
            {
                return View(await PaginatedList<TraHangViewModel>.
                        CreateAsync(trahang, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
            }

        }
    }
}
