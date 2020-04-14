using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.PayLog.Model;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.Model;
using Oryx.Utilities.SentryIO;

namespace Oryx.Content.Portal.Controllers.paynotfiy
{
    public class payapiController : Controller
    {
        public UserAccountBusiness userAccountBisness { get; set; }
        public payapiController(UserAccountBusiness _userAccountBisness)
        {
            userAccountBisness = _userAccountBisness;
        }

        public async Task<IActionResult> notify(payapiNotifyModel model)
        {
            try
            {

                DateTime? endTime = null;
                switch (model.price)
                {
                    case 0.01:
                    case 30.00:
                        endTime = DateTime.Now.AddDays(30);
                        break;
                    case 85.00:
                        endTime = DateTime.Now.AddMonths(3);
                        break;
                    case 100:
                        endTime = DateTime.Now.AddMonths(6);
                        break;
                    case 180:
                        endTime = DateTime.Now.AddYears(1);
                        break;
                    default:
                        endTime = null;
                        break;
                }

                var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                SentryLog.Log(jsonStr);
                var orderId = Guid.Parse(model.orderid);
                var orderModel = userAccountBisness.userAccountAccessor.All<PayAPILog>().FirstOrDefault(x => x.Id == orderId);
                orderModel.Statue = PayAPILogStatus.End;
                orderModel.PayAPIOrderId = model.paysapi_id;
                orderModel.EndTime = endTime;

                var userInfo = await userAccountBisness.userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == orderModel.UserAccountId, "InviteOrigin");
                if (userInfo?.InviteOrigin?.InviteKey == "lnn")
                {
                    orderModel.EndTime = DateTime.Now.AddYears(1);
                }
                await userAccountBisness.userAccountAccessor.SaveAsync();
            }
            catch (Exception exc)
            {
                SentryLog.Log(exc);
            }
            return Content("success");

        }

        public async Task<IActionResult> GetKey(double price)
        {
            return Json(new { });
        }
    }
}