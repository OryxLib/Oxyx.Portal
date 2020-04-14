using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Portal.Filters;
using Oryx.PayLog.Model;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.Model;
using Oryx.Utilities.Security;

namespace Oryx.Content.Portal.Pages.payapi
{
    [TypeFilter(typeof(PageAuthentication))]
    public class IndexModel : PageModel
    {
        public UserAccountBusiness userAccountBisness { get; set; }
        public IndexModel(UserAccountBusiness _userAccountBisness)
        {
            userAccountBisness = _userAccountBisness;
        }
        public string uid = "4ddf11be681659459de7f3e5";
        public string token = "ef79623ef89c25b22db58f5cc4effc8d";
        public string notifyUrl = "http://benzijia.com/payapi/notify";
        public string returnUrl = "http://benzijia.com/account/Info";
        public string Key = "";
        public string OrderId = "";
        public string GoodsName = "";
        public string OrderUId = "趣漫画会员";
        public double price = 0;
        public int istype = 1;
        public string UserId { get; set; }

        //1：支付宝；2：微信支付
        public async Task OnGet(int num = 1, int _istype = 2)
        {
            await HttpContext.Session.LoadAsync();
            UserId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            var dbTable = userAccountBisness.userAccountAccessor.All<PayAPILog>();
            var _userId = Guid.Parse(UserId);
            var sortedCollection = new SortedDictionary<string, string>();
            var userInfo = await userAccountBisness.userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == _userId, "InviteOrigin");
            //是否存在未处理订单
            var hasOrder = dbTable.Any(x => x.Statue != PayAPILogStatus.End || x.Statue != PayAPILogStatus.Error && x.UserAccountId == _userId);
            if (num < 30)
            {
                num = 30;
            }
            var orderId = Guid.NewGuid();
            if (!hasOrder)
            {
                dbTable.Add(new PayAPILog
                {
                    CreateTime = DateTime.Now,
                    Id = orderId,
                    PayNum = num,
                    Statue = PayAPILogStatus.Create,
                    UserAccountId = _userId
                });
                await userAccountBisness.userAccountAccessor.SaveAsync();
            }
            OrderId = orderId.ToString();
            GoodsName = "趣漫画会员支付";
            price = num;

            istype = _istype;
            if (userInfo?.InviteOrigin?.InviteKey == "lnn")
            {
                price = 0.01;
            }
            sortedCollection.Add("goodsname", GoodsName);
            sortedCollection.Add("price", price.ToString());
            sortedCollection.Add("istype", _istype.ToString());
            sortedCollection.Add("orderid", orderId.ToString());
            sortedCollection.Add("orderuid", OrderUId);
            sortedCollection.Add("token", token);
            sortedCollection.Add("return_url", returnUrl);
            sortedCollection.Add("notify_url", notifyUrl);
            sortedCollection.Add("uid", uid);
            var stringKey = new StringBuilder();
            foreach (var item in sortedCollection)
            {
                stringKey.Append(item.Value);
            }
            var tmpStr = stringKey.ToString();
            Key = MD5Tool.GetMD532(tmpStr);
        }
    }
}