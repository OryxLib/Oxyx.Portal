using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oryx.Shop.Business.Bs.Order; 
using Oryx.Wx.Pay;
using Oryx.Wx.Pay.ViewModel;
using Oryx.Wx.Pay.WxPayAPI;

namespace Oryx.Content.Portal.Controllers
{
    public class QRCodeNotifyController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public OrderBs OrderBs { get; set; }

        ILogger<QRCodeNotifyController> logger { get; set; }

        public QRCodeNotifyController(IConfiguration _configuration,
            OrderBs _OrderBs,
            ILogger<QRCodeNotifyController> _logger)
        {
            Configuration = _configuration;
            OrderBs = _OrderBs;
            logger = _logger;
        }

        public async Task<IActionResult> GetImg()
        {

            var pay = new NativePay();
            var str = pay.GetPrePayUrl(Guid.NewGuid().ToString());
            var stream = (MemoryStream)await Task.Run(() => { return WeAppPayApi.MakeQRCode(str); });
            //var bytes = new byte[stream.Length];
            //await stream.WriteAsync(bytes, 0, bytes.Length);
            //System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "testqrcode2.jpg", bytes);
            var resStream = new MemoryStream(stream.GetBuffer());
            return new FileStreamResult(resStream, "image/jpeg");

            //return File(stream, "image/jpeg");
        }

        public async Task<IActionResult> Order()
        {
            var streamReader = new StreamReader(Request.Body);
            var bodyStr = streamReader.ReadToEnd();
            Console.WriteLine(bodyStr);
            var pay = new NativeNotify();
            WxPayData data = new WxPayData();
            data.FromXml(bodyStr);
            var resultXml = pay.ProcessNotify(data);

            //WxPayData data = new WxPayData();
            //var dataXml = data.FromXml(bodyStr); 
            //var openId = dataXml["openid"].ToString();
            //var productId = dataXml["product_id"].ToString();
            //Console.WriteLine(Request.QueryString);
            //var vm = new UnifierOrderViewModel()
            //{
            //    OpenId = openId,
            //    Body = "ceshi ",
            //    Attach = "丸子家族",
            //    OutTradeNo = string.IsNullOrEmpty(productId) ? Guid.NewGuid().ToString() : productId.ToString(),
            //    Tag = "",
            //    TotalFee = (int)(1 * 100),
            //    NotifyUrl = "http://cartoon.oryxl.com/QRCodeNotify/Result"
            //};
            //Console.WriteLine(vm.OutTradeNo);
            ////orderEntity.OutTradeNo = outtradeno;
            ////await OrderBs.Update(orderEntity);
            ////var packageStr = WeAppPayApi.UnifiedOrder(vm);
            //await Task.Run(() => WeAppPayApi.UnifiedOrder(vm));
            return Content(resultXml);
        }

        public async Task<IActionResult> Result()
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
                        ordyerEntity.Status = Shop.Model.Shop.Orders.OrderStatus.Successful;
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