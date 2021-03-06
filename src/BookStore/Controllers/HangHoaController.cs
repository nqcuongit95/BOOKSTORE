﻿using BookStore.Models;
using BookStore.Resources;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize(Roles = "NhanVienKhoHang,NhanVienBanHang,Admin")]
    [Route("HangHoa")]
    public class HangHoaController : Controller
    {
        private readonly IBookStoreData _bookStoreData = null;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private IHostingEnvironment _environment;

        private string controller;
        private string action;

        public HangHoaController(
            IBookStoreData bookStoreData,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IHostingEnvironment environment)
        {
            _bookStoreData = bookStoreData;
            _sharedLocalizer = sharedLocalizer;
            _environment = environment;
        }

        #region Index

        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            AddInfoToViewData();

            try
            {
                var result = await NonamePaginatedList<HangHoa>.CreateAsync(
                    _bookStoreData.GetHangHoa(null, null), page ?? 1, pageSize ?? 10);

                return View(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet, ActionName("APISearch")]
        [Route("APISearch")]
        public async Task<IActionResult> APISearch(string search)
        {
            try
            {
                SearchResult result = new SearchResult();
                List<HangHoa> content = await _bookStoreData
                    .GetHangHoa(search, "Use").ToListAsync();

                foreach (var i in content)
                    result.Results.Add(new
                    {
                        Title = i.TenHangHoa,
                        Description = string.Format(
                            _sharedLocalizer["Price: {0} Available: {1}"],
                            (int)i.GiaBanLe, i.TonKho),
                        Id = i.Id,
                        TenHangHoa = i.TenHangHoa,
                        TonKho = i.TonKho,
                        GiaNhap = i.GiaNhap
                    });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Index

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
            [Bind("GiaBanLe,GiaBanSi,GiaKhoiTao,GiaNhap,LoaiHangHoaId,NhaCungCapId,NhanHieuId,TenHangHoa,TonKho")]
        HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties,
            bool? modal, bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            if (ModelState.IsValid)
            {
                try
                {
                    hangHoa.NgayLap = DateTime.Now;
                    hangHoa.GiaNhap = hangHoa.GiaNhap ?? 0;
                    hangHoa.GiaBanSi = hangHoa.GiaBanSi ?? 0;
                    hangHoa.GiaBanLe = hangHoa.GiaBanLe ?? 0;
                    hangHoa.DaBan = 0;
                    hangHoa.TrangThai = new TrangThai()
                    {
                        VietTat = "Use",
                        Loai = "HangHoa"
                    };

                    await _bookStoreData.AddHangHoa(hangHoa, properties);

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
                            new { id = hangHoa.Id });
                    else
                        message.Results["Current"] = new
                        {
                            value = hangHoa.Id,
                            name = hangHoa.TenHangHoa
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

        #endregion Create

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

        #endregion Details

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
            [Bind("Id,GiaBanLe,GiaBanSi,GiaKhoiTao,GiaNhap,LoaiHangHoaId,NhaCungCapId,NhanHieuId,TenHangHoa,TonKho,TrangThaiId")]
        HangHoa hangHoa,
            ICollection<ChiTietHangHoa> properties,
            bool? modal,
            bool? redirect)
        {
            AddInfoToViewData();

            bool isModal = modal ?? false;
            bool isRedirect = redirect ?? true;

            Message message = new Message();

            if (id != hangHoa.Id)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer["Invalid"];
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    hangHoa.TrangThai =
                        await _bookStoreData.GetTrangThaiById(hangHoa.TrangThaiId);

                    if (hangHoa.TrangThai.Loai != "HangHoa")
                        throw new Exception("Invalid");

                    await _bookStoreData.UpdateHangHoa(hangHoa, properties);

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
                            new { id = hangHoa.Id });
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

        #endregion Edit

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
                await _bookStoreData.DeleteHangHoa(id);

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

        #endregion Delete

        #region Other

        [Route("UploadAjax")]
        [HttpPost]
        public async Task<IActionResult> UploadAjax(int? id)
        {
            Message message = new Message();

            if (id == null)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer["NotFound"];
            }

            try
            {
                HangHoa hangHoa = await _bookStoreData.GetHangHoaById(id);

                hangHoa.TrangThai =
                    await _bookStoreData.GetTrangThaiById(hangHoa.TrangThaiId);

                if (hangHoa.TrangThai.Loai != "HangHoa")
                    throw new Exception("Invalid");

                if (hangHoa == null)
                {
                    message.Type = MessageType.Error;
                    message.Header = _sharedLocalizer["DefaultErrorHeader"];
                    message.Content = _sharedLocalizer["NotFound"];
                }

                Random random = new Random();
                var file = Request.Form.Files[0];
                string temp = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');

                string fileName = new string(
                    Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50)
                    .Select(s => s[random.Next(s.Length)]).ToArray())
                    + temp;
                string fullFileName = _environment.WebRootPath + $@"\uploads\{fileName}";

                while (System.IO.File.Exists(fullFileName))
                {
                    fileName = new string(
                    Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50)
                    .Select(s => s[random.Next(s.Length)]).ToArray())
                    + temp;
                    fullFileName = _environment.WebRootPath + $@"\uploads\{fileName}";
                }

                if (hangHoa.ImageUrl != null)
                    System.IO.File.Delete(
                        _environment.WebRootPath + hangHoa.ImageUrl);

                using (FileStream fs = System.IO.File.Create(fullFileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                await _bookStoreData
                    .UpdateHangHoaImageUrl(id ?? 0, $@"\uploads\{fileName}");

                message.Results["Reload"] = true;

                message.Type = MessageType.Success;
                message.Header = _sharedLocalizer["Success"];
                message.Content = string.Format(
                    "{0} {1}",
                    _sharedLocalizer["EditImage"],
                    _sharedLocalizer["Success"].Value.ToLower());
            }
            catch (Exception ex)
            {
                message.Type = MessageType.Error;
                message.Header = _sharedLocalizer["DefaultErrorHeader"];
                message.Content = _sharedLocalizer[ex.GetType().FullName];
            }

            return Json(message);
        }

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
                HangHoa hangHoa = await _bookStoreData.GetHangHoaById(id);

                hangHoa.LoaiHangHoa =
                    await _bookStoreData.GetLoaiHangHoaById(hangHoa.LoaiHangHoaId);
                hangHoa.NhaCungCap =
                    await _bookStoreData.GetNhaCungCapById(hangHoa.NhaCungCapId);
                hangHoa.NhanHieu =
                    await _bookStoreData.GetNhanHieuById(hangHoa.NhanHieuId);
                hangHoa.ChiTietHangHoa =
                    await _bookStoreData.GetChiTietHangHoa(id).ToArrayAsync();
                hangHoa.TrangThai =
                    await _bookStoreData.GetTrangThaiById(hangHoa.TrangThaiId);

                if (hangHoa == null)
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
                        return PartialView(GetUrlPartialPartial(), hangHoa);
                    else
                        return View(hangHoa);
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

        #endregion Other
    }
}