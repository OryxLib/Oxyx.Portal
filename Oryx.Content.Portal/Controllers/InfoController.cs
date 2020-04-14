using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Common.Business;
using Oryx.Common.Model.Feedback;
using Oryx.Content.Portal.Filters;
using Oryx.UserAccount.Business;
using Oryx.Utilities;
using Oryx.ViewModel;
using Oryx.ViewModel.UserInfo;

namespace Oryx.Content.Portal.Controllers
{
    public class InfoController : Controller
    {
        UserAccountBusiness userAccountBusiness { get; set; }
        FeedbackBusiness feedbackBusiness;
        public InfoController(UserAccountBusiness _userAccountBusiness, FeedbackBusiness _feedbackBusiness)
        {
            userAccountBusiness = _userAccountBusiness;
            feedbackBusiness = _feedbackBusiness;
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> DetailInfo()
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () => await userAccountBusiness.GetUserInfo(userId));
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetOtherUserInfo(Guid userId)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var userInfo = await userAccountBusiness.GetUserInfo(userId);
                return new
                {
                    userId = userInfo.userId,
                    avatar = userInfo.avatar,
                    nickName = userInfo.nickName
                };
            });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> UpdateInfo([FromBody]UpdateUserInfoViewModel udpateModel)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () => await userAccountBusiness.UpdateUserInfo(userId, udpateModel));
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PushFeedback([FromBody] UserFeedback feedback)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            feedback.CreateTime = DateTime.Now;
            feedback.Id = Guid.NewGuid();
            feedback.UserAccountId = userId;

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await feedbackBusiness.AddFeedback(feedback);
            });
            return Json(apiMsg);
        } 
    }
}