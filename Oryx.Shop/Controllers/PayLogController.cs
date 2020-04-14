using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Shop.Business.Bs.PayLog;

namespace Oryx.Shop.Controllers
{
    public class PayLogController : Controller
    {
        public PayLogBs PayLogBs { get; set; }

        public PayLogController(PayLogBs _PayLogBs)
        {
            PayLogBs = _PayLogBs;
        }

        public async Task<IActionResult> RequestPay(Guid orderId)
        {
            var result = await PayLogBs.GetWxPayPackage(orderId);
            return Json(result);
        }
    }
}