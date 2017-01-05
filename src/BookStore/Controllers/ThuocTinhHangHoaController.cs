using BookStore.Models;
using BookStore.Resources;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    
    [Route("HangHoa/ThuocTinhHangHoa")]
    public class ThuocTinhHangHoaController : Controller
    {
        private readonly IBookStoreData _bookStoreData;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private string controller;
        private string action;

        public ThuocTinhHangHoaController(
            IBookStoreData bookStoreData,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _bookStoreData = bookStoreData;
            _sharedLocalizer = sharedLocalizer;
        }

        #region Index
        [HttpGet, ActionName("APISearch")]
        [Route("APISearch")]
        public IActionResult APISearch(int? loaiHangHoaId)
        {
            try
            {
                SearchResult result = new SearchResult();
                var content = _bookStoreData.GetThuocTinhHangHoa(loaiHangHoaId);

                foreach (var i in content)
                    result.Results.Add(new {
                        Title = i.TenThuocTinh
                    });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Other
        private IActionResult C(bool? modal, bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            try
            {
                ViewData["Modal"] = isModal;
                ViewData["Redirect"] = isRedirect;

                if (isModal)
                    return PartialView(GetUrlPartialPartial());
                else
                    return View();
            }
            catch (Exception ex)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer[ex.GetType().FullName];
            }

            return Json(message);
        }

        private async Task<IActionResult> RUD(int? id, bool? modal, bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            if (id == null)
            {
                if (isModal)
                {
                    message.Type = MessageType.Error;
                    message.Header = _sharedLocalizer["DefaultErrorHeader"];
                    message.Content = _sharedLocalizer["NotFound"];
                }
            }

            try
            {
                ThuocTinhHangHoa thuocTinhHangHoa = await _bookStoreData.GetThuocTinhHangHoaById(id);

                if (thuocTinhHangHoa == null)
                {
                    if (isModal)
                    {
                        message.Type = MessageType.Error;
                        message.Header = _sharedLocalizer["DefaultErrorHeader"];
                        message.Content = _sharedLocalizer["NotFound"];
                    }
                }
                else
                {
                    ViewData["Modal"] = isModal;
                    ViewData["Redirect"] = isRedirect;

                    if (isModal)
                        return PartialView(GetUrlPartialPartial(), thuocTinhHangHoa);
                    else
                        return View(thuocTinhHangHoa);
                }
            }
            catch (Exception ex)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer[ex.GetType().FullName];
            }

            if (isModal)
                return Json(message);
            else
                return RedirectToAction("Index");
        }

        private void AddInfoToViewData()
        {
            controller = RouteData.Values["controller"].ToString();
            action = RouteData.Values["action"].ToString();

            ViewData["Title"] = _sharedLocalizer[controller];
            ViewData["Action"] =
                _sharedLocalizer[action] + " " +
                _sharedLocalizer[controller].Value.ToLower();
        }

        private string GetUrlPartialPartial()
        {
            return string.Format(
                "~/Views/Shared/{0}/_{1}Partial.cshtml",
                controller, action);
        }
        #endregion
    }
}
