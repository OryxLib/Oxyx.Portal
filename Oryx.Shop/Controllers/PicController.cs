using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Utilities;
using Oryx.ViewModel;

namespace Oryx.Shop.Controllers
{
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
    }
}