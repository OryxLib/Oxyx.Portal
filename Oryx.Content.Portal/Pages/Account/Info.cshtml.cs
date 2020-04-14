using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Portal.Filters;
using Oryx.PayLog.Model;
using Oryx.UserAccount.Business;

namespace Oryx.Content.Portal.Pages.Account
{
    [TypeFilter(typeof(PageAuthentication))]
    public class InfoModel : PageModel
    {
        public UserAccountBusiness userAccountBisness { get; set; }
        public string UserId { get; set; }

        public InfoModel(UserAccountBusiness _userAccountBisness)
        {
            userAccountBisness = _userAccountBisness;
        }
        public string OrderId = string.Empty;

        public async Task OnGet()
        {
            await HttpContext.Session.LoadAsync();
            UserId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            if (Request.Query.ContainsKey("orderid"))
            {
                OrderId = Request.Query["orderid"];
                await HttpContext.Session.LoadAsync();
                UserId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
                var _userId = Guid.Parse(UserId);
                var payLog = userAccountBisness.userAccountAccessor.All<PayAPILog>().FirstOrDefault(x => x.UserAccountId == _userId);
            }
        }
    }
}