using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Oryx.Shop.Business.Bs.Order;
using Oryx.Wx.MiniApp;
using Oryx.Wx.Pay;
using Oryx.Wx.Pay.ViewModel;
using Oryx.Wx.Pay.WxPayAPI;

namespace Oryx.Shop.BusinessApi.Controllers.WxApp
{
    public class WxAppController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public OrderBs OrderBs { get; set; }

        ILogger<WxAppController> logger { get; set; }

        public WxAppController(IConfiguration _configuration,
            OrderBs _OrderBs,
            ILogger<WxAppController> _logger)
        {
            Configuration = _configuration;
            OrderBs = _OrderBs;
            logger = _logger;
        }
        public async Task<IActionResult> Login(string code)
        {
            var result = await Autherize.CheckSession(Configuration["WxWeApp_Shopping_AppId"], Configuration["WxWeApp_Shopping_Secret"], code);
            return Content(JObject.Parse(result)["openid"].ToString());
        }

        public async Task<IActionResult> UiniferOrder(Guid orderId)
        {
            try
            {
                var orderEntity = await OrderBs.GetDetailData(orderId);

                var totalFee = orderEntity.OrderGoods.Select(x => new
                {
                    Fee = x.Good.Price * x.Number
                }).Sum(x => x.Fee);

                var outtradeno = WeAppPayApi.GenOutTradeNo();

                var vm = new UnifierOrderViewModel()
                {
                    OpenId = orderEntity.UserAccountEntry.WeChat.OpenIdMapping.FirstOrDefault(x => x.Source == "丸子家族")?.OpenId,
                    Body = "ceshi ",
                    Attach = "丸子家族",
                    OutTradeNo = outtradeno,
                    Tag = "",
                    TotalFee = (int)(totalFee * 100)
                };
                orderEntity.OutTradeNo = outtradeno;
                await OrderBs.Update(orderEntity);
                var packageStr = WeAppPayApi.UnifiedOrder(vm);

                //return Json(packageStr);
                return Content(packageStr, "application/json");

            }
            catch (Exception exc)
            {
                logger.LogError(exc.Message);
                return Content(exc.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> NotifyUrl()
        {
            try
            {
                WxPayData notifyData;
                var responseStr = WeAppPayApi.ProcessNotify(HttpContext.Request.Body, out notifyData);

                if (notifyData != null)
                {

                    var out_trade_no = notifyData.GetValue("out_trade_no").ToString();
                    var ordyerEntity = await OrderBs.GetDetailData(x => x.OutTradeNo == out_trade_no);
                    if (ordyerEntity != null)
                    {
                        ordyerEntity.Status = Model.Shop.Orders.OrderStatus.Successful;
                        await OrderBs.Update(ordyerEntity);
                        await OrderBs.WriteLog(ordyerEntity);
                    }
                    else
                    {
                        logger.LogError("微信服务器数据处理失败。");
                        return Content("Faulth");
                    }
                }

                return Content(responseStr);
            }
            catch (Exception exc)
            {
                logger.LogError(exc.Message);
                return Content(exc.Message);
            }
        }
    }
}