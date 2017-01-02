using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using BookStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Helper;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private IBookStoreData _bookStoreData;
        private UserManager<Staff> _userManager;
        private IViewRenderService _viewRenderService;

        public SaleController(IBookStoreData bookStoreData,
                              UserManager<Staff> userManager,
                              IViewRenderService viewRenderService)
        {
            _bookStoreData = bookStoreData;
            _userManager = userManager;
            _viewRenderService = viewRenderService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View("Index", user.FullName);
        }

        //find customers
        public async Task<IActionResult> FindCustomer(string val)
        {
            //add action: create new customer
            UrlHelper urlHelper = new UrlHelper(this.ControllerContext);
            string url = urlHelper.Action("Sale", "CreateCustomer");

            var addCustomerAction = new BookStore.ViewModels.Action
            {
                Text = "Thêm mới khách hàng",
                Url = url
            };

            if (val == null)
            {

                var result1 = new List<CustomerFilterViewModel>();
                result1.Add(new CustomerFilterViewModel
                {
                    Name = "Khách Vãng Lai",
                    Phone = "",
                    Address = "",
                    Id = 1   //hard code id for simplicity sake                  
                });

                var model1 = new CustomerFilterResults { Results = result1 };
                model1.NewCustomer = addCustomerAction;

                return Json(model1);
            }

            var model = await _bookStoreData.FindCustomer(val);
            model.NewCustomer = addCustomerAction;

            return Json(model);
        }

        public async Task<IActionResult> FindProduct(string keyword)
        {

            if (keyword == null)
            {
                var model = await _bookStoreData.GetBestSellingGoods(4, Helper.TimeEnum.Week, ProductType.Both);

                var result1 = new ProductFilterResults { Results = model };

                return Json(result1);
            }

            var result = await _bookStoreData.FindProduct(keyword);

            return Json(result);
            //return PartialView("_ProductResults",model);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            var loaiKhachHang = _bookStoreData.GetAllLoaiKhachHang();
            var model = new KhachHangViewModel();
            model.LoaiKhachHang = new SelectList(loaiKhachHang, "Id", "TenLoaiKhachHang", 3);

            return PartialView("_CreateCustomer", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(KhachHangViewModel model)
        {
            Notification notify;
            string modalAsString;
            if (ModelState.IsValid)
            {
                model.KhachHang.NgayLap = DateTime.Now;
                var id = _bookStoreData.CreateCustomer(model.KhachHang);

                notify = new Notification
                {
                    Icon = "checkmark",
                    Title = "Thành Công",
                    Content = "Thêm mới khách hàng thành công",
                    MessageType = "positive",
                    Button = "Hoàn tất"

                };

                modalAsString = await _viewRenderService.RenderToStringAsync("Sale/_Notify", notify);
                return Json(new { modal = modalAsString, id = id });

            }

            notify = new Notification
            {
                Icon = "remove",
                Title = "Thất bại",
                Content = "Có lỗi xảy ra, vui lòng thử lại",
                MessageType = "negative",
                Button = "Quay lại"
            };

            modalAsString = await _viewRenderService.RenderToStringAsync("Sale/_Notify", notify);
            return Json(new { modal = modalAsString });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LastCreatedCustomer(int id)
        {
            var model = await _bookStoreData.GetCustomerById(id);

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePriceType(string[] productIds, int priceType)
        {
            List<ProductPriceViewModel> model = new List<ProductPriceViewModel>();

            foreach (var id in productIds)
            {
                var result = await _bookStoreData.GetPrice(int.Parse(id), priceType);
                if (result != null)
                {
                    model.Add(result);
                }
            }

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayInvoice(InvoiceViewModel invoice,
                                        List<ProductBuyingDetailsViewModel> productDetails)
        {
            if (ModelState.IsValid)
            {
                var status = await _bookStoreData.AddInvoice(invoice, productDetails);

                if (status.Item1)
                {
                    var model1 = new Notification
                    {
                        Title = "Thành Công",
                        Content = "Thêm đơn hàng thành công",
                        Button = "Hoàn tất",
                        Icon = "checkmark",
                        MessageType = "positive",
                        ExtraAction = "In hóa đơn",
                        ExtraData = status.Item2.ToString()
                    };
                    return PartialView("_NotifyInvoice", model1);
                }
            }

            var model = new Notification
            {
                Title = "Thất bại",
                Content = "Có lỗi xảy ra",
                Button = "Quay lại",
                Icon = "remove",
                MessageType = "negative"
            };
            return PartialView("_Notify", model);

        }

        public async Task<IActionResult> PrintBill(int id, decimal pay)
        {
            if (ModelState.IsValid)
            {

                var model = await _bookStoreData.GetBill(id);

                model.CustomerPaid = pay;
                var change = pay - model.TotalValueAfterDiscount;
                model.CustomerChange = change > 0 ? change : 0;

                return PartialView("_Bill", model);
            }

            var modelError = new Notification
            {
                Title = "Thất bại",
                Content = "Có lỗi xảy ra, không thể in đơn hàng",
                Button = "Quay lại",
                Icon = "remove",
                MessageType = "negative"
            };
            return PartialView("_Notify", modelError);
        }

    }
}
