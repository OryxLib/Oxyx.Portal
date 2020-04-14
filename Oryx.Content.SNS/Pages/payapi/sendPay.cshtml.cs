using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Portal.Filters;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.Model;

namespace Oryx.Content.Portal.Pages.payapi
{
    [TypeFilter(typeof(PageAuthentication))]
    public class sendPayModel : PageModel
    {
        public UserAccountBusiness userAccountBisness { get; set; }
        public bool IsSuperVip = false;
        public sendPayModel(UserAccountBusiness _userAccountBisness)
        {
            userAccountBisness = _userAccountBisness;
        }
        public async Task OnGet()
        {
            await HttpContext.Session.LoadAsync();
            var UserId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            //var dbTable = userAccountBisness.userAccountAccessor.All<PayAPILog>();
            var _userId = Guid.Parse(UserId);
            var userInfo = await userAccountBisness.userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == _userId, "InviteOrigin");
            if (userInfo?.InviteOrigin?.InviteKey == "lnn")
            {
                IsSuperVip = true;
            }
        }
    }
}