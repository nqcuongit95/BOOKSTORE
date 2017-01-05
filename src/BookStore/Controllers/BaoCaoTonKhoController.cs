using BookStore.Resources;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize(Roles = "NhanVienKhoHang,NhanVienBanHang,Admin")]
    [Route("BaoCao/BaoCaoTonKho")]
    public class BaoCaoTonKhoController : Controller
    {
        private readonly IBookStoreData _bookStoreData = null;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private string controller;
        private string action;

        public BaoCaoTonKhoController(
            IBookStoreData bookStoreData,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _bookStoreData = bookStoreData;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<ActionResult> Index()
        {
            AddInfoToViewData();

            BaoCaoTonKhoViewModel model = new BaoCaoTonKhoViewModel();

            model.HangHoa = await _bookStoreData.GetHangHoa(null, null).ToListAsync();

            foreach (var item in model.HangHoa)
            {
                model.TotalInventory += item.TonKho;

                decimal giaNhap = item.GiaNhap ?? 0;

                model.TotalValueInventory += giaNhap * (decimal)item.TonKho;
            }

            return View(model);
        }

        #region Other

        private void AddInfoToViewData()
        {
            controller = RouteData.Values["controller"].ToString();
            action = RouteData.Values["action"].ToString();

            ViewData["Title"] = _sharedLocalizer[controller];
            ViewData["Action"] =
                _sharedLocalizer[action] + " " +
                _sharedLocalizer[controller].Value.ToLower();
        }

        #endregion Other
    }
}