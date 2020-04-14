using Microsoft.AspNetCore.Mvc;
using Oryx.Content.Business;
using Oryx.Utilities;
using Oryx.ViewModel;
using Oryx.ViewModel.Editor;
using System;
using System.Threading.Tasks;

namespace Oryx.Content.Admin.Controllers
{
    public class PicController : Controller
    {
        FileEntryBusiness fileEntryBusiness;
        public PicController(FileEntryBusiness _fileEntryBusiness)
        {
            fileEntryBusiness = _fileEntryBusiness;
        }
        public IActionResult Token()
        {
            var token = QiniuTool.GenerateToken();
            var result = new ApiMessage();
            result.Data = token;
            result.Message = "success!";
            result.Success = true;
            return Json(result);
        }

        public async Task<IActionResult> Upload()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var files = Request.Form.Files;

                return await fileEntryBusiness.SaveLocal(files, "Image", "");
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> Get(Guid Id)
        {
            var fileStream = await fileEntryBusiness.GetFile(Id);
            if (fileStream.Length == 0)
            {
                return Content("Can not find the document.");
            }
            return File(fileStream, "image/jpeg");
        }
    }
}