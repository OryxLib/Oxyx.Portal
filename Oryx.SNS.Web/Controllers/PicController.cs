using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.FacePP;
using Oryx.Utilities;
using Oryx.ViewModel;

namespace Oryx.SNS.Web.Controllers
{
    public class PicController : Controller
    {
        public IActionResult Token()
        {
            var result = new ApiMessage();
            try
            {
                var token = QiniuTool.GenerateToken();
                result.Data = token;
                result.Message = "success!";
                result.Success = true;
            }
            catch (Exception exc)
            {
                result.SetFault(exc);
            }

            return Json(result);
        }

        public IActionResult Yanzhi(string imgurl, string token)
        {
            //HttpContext.Session.Set(;
            var resStr = FacePP.FacePP.CheckYanzhi(new Uri(imgurl));
            return Content(resStr, "application/json");
        }
    }
}