using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class DonHangController : Controller
    {
        // GET: /<controller>/
        private IBookStoreData _bookStoreData;
        public DonHangController(IBookStoreData bookStoreData)
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

            var customers = _bookStoreData.GetAllDonHang();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.TenKhachHang.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.ID);
                    ViewData["SortDirection"] = "down";
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.NgayLap);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.NgayLap);
                    break;
                default:
                    customers = customers.OrderBy(c => c.ID);
                    ViewData["SortDirection"] = "up";
                    break;
            }

            int pageSize = 9;
            int numberOfDisplayPages = 5;

            return View(await PaginatedList<DonHangViewModel>.
                        CreateAsync(customers, page ?? 1, pageSize,
                                    numberOfDisplayPages,
                                    firstShowedPage, lastShowedPage));
        }


    }
}
