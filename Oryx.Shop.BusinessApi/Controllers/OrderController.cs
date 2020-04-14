using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Shop.Business.Bs.Order;
using Oryx.Shop.Model.Shop.Orders;
using Oryx.ViewModel.Shop.Orders;

namespace Oryx.Shop.BusinessApi.Controllers
{
    public class OrderController : Controller
    {
        public OrderBs OrderBs { get; set; }

        public OrderController(OrderBs _OrderBs)
        {
            OrderBs = _OrderBs;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateOrderModel createOrderModel)
        {
            var result = await OrderBs.CreateOrder(createOrderModel);
            return Json(result);
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            var result = await OrderBs.GetDetail(Id);
            return Json(result);
        }

        public async Task<IActionResult> List(Guid Token, OrderStatus status, int skipCount = 0, int pageSize = 10)
        {
            var result = await OrderBs.OrderList(Token, status, skipCount);
            return Json(result);
        }

        public async Task<IActionResult> SendExpress(Guid Id)
        {
            var result = await OrderBs.SendExpress(Id);
            return Json(result);
        }
    }
}