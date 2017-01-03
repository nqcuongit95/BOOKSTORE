using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("UploadFiles")]
    public class UploadFilesController : Controller
    {
        private IHostingEnvironment _environment;

        public UploadFilesController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Upload")]
        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = _environment.WebRootPath + $@"\uploads\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            ViewBag.Message = "asas";
            return View();
        }

        [Route("UploadAjax")]
        [HttpPost]
        public IActionResult UploadAjax()
        {
            long size = 0;
            var file = Request.Form.Files[0];
            var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');
            filename = _environment.WebRootPath + $@"\uploads\{filename}";
            size += file.Length;
            using (FileStream fs = System.IO.File.Create(filename))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            string message = filename;
            return Json(message);
        }
    }
}