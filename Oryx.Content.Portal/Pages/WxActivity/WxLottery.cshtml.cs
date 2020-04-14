using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.UserActivity.Business;
using Oryx.UserAccount.Business;
using Oryx.ViewModel.UserInfo;
using Oryx.Content.Portal.Filters;
using Oryx.Utilities.SentryIO;

namespace Oryx.Content.Portal.Pages.WxActivity
{
    [TypeFilter(typeof(PageAuthentication))]
    public class WxLotteryModel : PageModel
    {
        private readonly UserAccountBusiness userAccountBusiness;
        private readonly ActivityBusiness activityBusiness;

        public UserInfoOutputViewModel UserInfo { get; set; }

        public bool HasPlay { get; set; }

        public string ActivityNamek = "WxLotteryFirst";

        public string UserId { get; set; }

        public WxLotteryModel(UserAccountBusiness _userAccountBusiness,
            ActivityBusiness _activityBusiness)
        {
            userAccountBusiness = _userAccountBusiness;
            activityBusiness = _activityBusiness;
        }

        public async Task OnGet()
        {
            await HttpContext.Session.LoadAsync();
            var userId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            UserId = userId;
            SentryLog.Log("test lottery userId;" + userId);
            //userId = "d14e4c49-5502-43df-8ce9-0bf93baef067";
            var _userId = Guid.Parse(userId);
            UserInfo = await userAccountBusiness.GetUserInfo(_userId);
            HasPlay = await activityBusiness.HasPlay(ActivityNamek, _userId);
        }

        public async Task<IActionResult> OnPostAsync(string name, string phone)
        {
            await HttpContext.Session.LoadAsync();
            var userId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            var _userId = Guid.Parse(userId);
            var userAccount = await userAccountBusiness.GetUserAccount(_userId, "Profile");
            await userAccountBusiness.SetPhone(_userId, phone);
            await userAccountBusiness.SetTrueName(_userId, name);

            await activityBusiness.SetActivityLog(ActivityNamek, _userId, name + ":" + phone);
            return RedirectToPage("/WxActivity/WxLottery");
        }
    }
}