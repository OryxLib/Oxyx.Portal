using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.FacePP;
using Oryx.Utilities;
using Oryx.ViewModel;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class PicController : Controller
    {
        public IActionResult Token()
        {
            var token = QiniuTool.GenerateToken();
            var result = new ApiMessage();
            result.Data = token;
            result.Message = "success!";
            result.Success = true;
            return Json(result);
        }

        public IActionResult Yanzhi(string imgurl, string token)
        {
            var resStr = FacePP.FacePP.CheckYanzhi(new Uri(imgurl));
            return Content(resStr, "application/json");
        }
    }
}