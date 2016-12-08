using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services;
using Microsoft.Extensions.Localization;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Localization;
using BookStore.Resources;
using Microsoft.AspNetCore.Routing;

namespace BookStore.Controllers
{
    [Route("HangHoa/LoaiHangHoa")]
    public class LoaiHangHoaController : Controller
    {
        private readonly IBookStoreData _bookStoreData = null;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private string controller;
        private string action;

        public LoaiHangHoaController(
            IBookStoreData bookStoreData,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _bookStoreData = bookStoreData;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            AddInfoToViewData();

            try
            {
                var result = await PaginatedList<LoaiHangHoa>.CreateAsync(
                    _bookStoreData.GetLoaiHangHoa(), page ?? 1, pageSize ?? 10);

                return View(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        #region Create
        [Route("Create")]
        public IActionResult Create(bool? modal, bool? redirect)
        {
            return C(modal, redirect);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateConfirmed")]
        public async Task<IActionResult> CreateConfirmed(
            [Bind("TenLoaiHangHoa")] LoaiHangHoa loaiHangHoa,
             bool? modal,
             bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookStoreData.AddLoaiHangHoa(loaiHangHoa);

                    message.Type = MessageType.Success;
                    message.Header = _sharedLocalizer["Success"];
                    message.Content = string.Format(
                        "{0} {1}",
                        _sharedLocalizer[action],
                        _sharedLocalizer[controller].Value.ToLower());

                    //if (isModal)
                    //    message.Results["Reload"] = true;

                    if (isRedirect)
                        message.Results["RedirectUrl"] = Url.Action(
                            "Details",
                            new { id = loaiHangHoa.Id });
                    else
                        message.Results["Current"] = new
                        {
                            value = loaiHangHoa.Id,
                            name = loaiHangHoa.TenLoaiHangHoa
                        };
                }
                catch (Exception ex)
                {
                    message.Type = MessageType.Error;
                    message.Header = _sharedLocalizer["DefaultErrorHeader"];
                    message.Content = _sharedLocalizer[ex.GetType().FullName];
                }
            }

            return Json(message);
        }
        #endregion

        #region Details
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            return await RUD(id, false, true);
        }

        [HttpPost, ActionName("Details")]
        [Route("Details")]
        public async Task<IActionResult> DetailsModal(int? id)
        {
            return await RUD(id, true, false);
        }
        #endregion

        #region Edit
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            return await RUD(id, false, true);
        }

        [HttpPost, ActionName("Edit")]
        [Route("Edit")]
        public async Task<IActionResult> EditModal(int? id, bool? redirect)
        {
            return await RUD(id, true, redirect);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditConfirmed")]
        public async Task<IActionResult> EditConfirmed(
            int id,
            [Bind("Id,TenLoaiHangHoa")] LoaiHangHoa LoaiHangHoa,
            bool? modal,
            bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            if (id != LoaiHangHoa.Id)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer["Invalid"];
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    await _bookStoreData.UpdateLoaiHangHoa(LoaiHangHoa);

                    message.Type = MessageType.Success;
                    message.Header = _sharedLocalizer["Success"];
                    message.Content = string.Format(
                        "{0} {1}",
                        _sharedLocalizer[action],
                        _sharedLocalizer[controller].Value.ToLower());

                    if (isModal)
                        message.Results["Reload"] = true;

                    if (isRedirect)
                        message.Results["RedirectUrl"] = Url.Action(
                            "Details",
                            new { id = LoaiHangHoa.Id });
                }
                catch (Exception ex)
                {
                    message.Type = MessageType.Error;
                    message.Header = _sharedLocalizer["DefaultErrorHeader"];
                    message.Content = _sharedLocalizer[ex.GetType().FullName];
                }
            }

            return Json(message);
        }
        #endregion

        #region Delete
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            return await RUD(id, false, true);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete")]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            return await RUD(id, true, false);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(
            int id,
            bool? modal,
            bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            try
            {
                await _bookStoreData.DeleteLoaiHangHoa(id);

                message.Type = MessageType.Warning;
                message.Header = _sharedLocalizer["Success"];
                message.Content = string.Format(
                    "{0} {1}",
                    _sharedLocalizer[action],
                    _sharedLocalizer[controller].Value.ToLower());

                if (isModal)
                    message.Results["Reload"] = true;

                if (isRedirect)
                    message.Results["RedirectUrl"] = Url.Action("Index");
            }
            catch (Exception ex)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer[ex.GetType().FullName];
            }

            return Json(message);
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
                LoaiHangHoa LoaiHangHoa = await _bookStoreData.GetLoaiHangHoaById(id);

                if (LoaiHangHoa == null)
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
                        return PartialView(GetUrlPartialPartial(), LoaiHangHoa);
                    else
                        return View(LoaiHangHoa);
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
