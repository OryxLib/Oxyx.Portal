using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Shop.Business.Bs.Order;
using Oryx.ViewModel.Shop.Orders;

namespace Oryx.Shop.Controllers
{
    public class OrderController : Controller
    {
        public OrderBs OrderBs { get; set; }

        public OrderController(OrderBs _OrderBs)
        {
            OrderBs = _OrderBs;
        }

        public async Task<IActionResult> SendExpress(Guid Id)
        {
            var result =await OrderBs.SendExpress(Id);
            return Json(result);
        }
    }
}