using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.CommonDbOperation;
using Oryx.SNS.Model;
using Oryx.SNS.Web.BaseControllers;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class TipController : BaseController<TipEntry>
    {
        public TipController(CommonAccessor _accessor) : base(_accessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}