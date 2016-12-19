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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private IBookStoreData _bookStoreData;
        private UserManager<Staff> _userManager;

        public SaleController(IBookStoreData bookStoreData,
                              UserManager<Staff> userManager)
        {
            _bookStoreData = bookStoreData;
            _userManager = userManager;
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
                var model = await _bookStoreData.GetBestSellingGoods(4, DateTime.Now);

                var result1 = new ProductFilterResults { Results = model };

                return Json(result1);
            }

            var result = await _bookStoreData.FindProduct(keyword);

            return Json(result);
            //return PartialView("_ProductResults",model);
        }

        public IActionResult CreateCustomer()
        {
            return PartialView("_CreateCustomer");
        }

        [HttpPost]
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
        public async Task<IActionResult> PayInvoice(InvoiceViewModel invoice,
                                        List<ProductBuyingDetailsViewModel> productDetails)
        {
            if (ModelState.IsValid)
            {
                var success = await _bookStoreData.AddInvoice(invoice, productDetails);

                if (success)
                {
                    var model1 = new Notification
                    {
                        Title = "Thành Công",
                        Content = "Thêm đơn hàng thành công",
                        Button = "Hoàn tất"
                    };
                    return PartialView("_Notify", model1);
                }
            }

            var model = new Notification
            {
                Title = "Thất bại",
                Content = "Có lỗi xảy ra",
                Button = "Quay lại"
            };
            return PartialView("_Notify", model);

        }
    }
}
