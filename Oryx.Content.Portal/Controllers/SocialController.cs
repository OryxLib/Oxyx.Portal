using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Content.Portal.Filters;
using Oryx.Social.Business;
using Oryx.UserAccount.Business;
using Oryx.Utilities.Redis;
using Oryx.ViewModel;
using Oryx.ViewModel.Social.Chat;

namespace Oryx.Content.Portal.Controllers
{
    public class SocialController : Controller
    {
        private readonly ChatBusiness chatBusiness;
        private readonly UserAccountBusiness userAccountBusiness;
        private readonly IDistributedCache distributedCache;
        private readonly UserSocialRelationshipBusiness userSocialRelationshipBusiness;
        public SocialController(ChatBusiness _chatBusiness,
            UserAccountBusiness _userAccountBusiness,
            UserSocialRelationshipBusiness _userSocialRelationshipBusiness,
            IDistributedCache _distributedCache)
        {
            chatBusiness = _chatBusiness;
            userAccountBusiness = _userAccountBusiness;
            distributedCache = _distributedCache;
            userSocialRelationshipBusiness = _userSocialRelationshipBusiness;
        }

        public async Task<IActionResult> ChatList(string accessToken)
        {
            try
            {
                var userIdDocument = await distributedCache.GetValue<string>(accessToken);
                Guid userId;
                var apiMsg = new ApiMessage();
                if (userIdDocument.ExpireTime < DateTime.Now)
                {
                    apiMsg.SetFault("AccessToken  Timeout");
                    apiMsg.ErrorCode = "503";
                }
                else if (Guid.TryParse(userIdDocument.Value, out userId))
                {
                    apiMsg = await ApiMessage.Wrap(async () =>
                    {
                        return await chatBusiness.GetChatList(userId);
                    });
                }
                else
                {
                    apiMsg.SetFault("Invalid accesstoken ");
                    apiMsg.ErrorCode = "503";
                }
                return Json(apiMsg);
            }
            catch (Exception exc)
            {
                return Content(exc.Message);
            }
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> ChatHistory(Guid toUserId, int skipCount = 0, int pageSize = 10)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await chatBusiness.GetChatHistory(toUserId, userId, skipCount, pageSize);
            });

            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> GetUnReadMsg()
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await chatBusiness.GetUnReadMsgNum(userId);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> UserList(string queryKey)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
             {
                 var userInfoList = await userAccountBusiness.QueryUserByKey(queryKey);
                 return userInfoList.Select(x => new
                 {
                     avatar = x.Avatar,
                     userId = x.Id,
                     nickName = x.NickName
                 });
             });

            return Json(apiMsg);
        }

        /// <summary>
        /// 因为前端暴露的是accessToken , 
        /// 所以fromid 实际为accessToken, 但ToId 为用户Id
        /// </summary>
        /// <param name="chatModel"></param>
        /// <returns></returns> 
        public async Task<IActionResult> SendTxtMsg([FromBody]TextChatMessageViewModel chatModel)
        {
            //var userIdDocument = await distributedCache.GetValue<string>(chatModel.FromId.ToString());
            //chatModel.FromId = Guid.Parse(userIdDocument.Value);
            var apiMsg = await ApiMessage.Wrap(async () =>
             {
                 await chatBusiness.SendMessage(chatModel);
             });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> GetUserSocialInfo(Guid? connectorUserId)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            Guid? owerUserId;
            if (!string.IsNullOrEmpty(userIdStr))
            {
                owerUserId = Guid.Parse(userIdStr);
            }
            else
            {
                owerUserId = null;
            }

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var userInfo = await userAccountBusiness.GetUserInfo(connectorUserId.Value);
                var isSubcrib = await userSocialRelationshipBusiness.CheckSubscib(owerUserId, connectorUserId.Value);
                return new
                {
                    userInfo,
                    isSubcrib
                };
            });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> GetUserRelationShipInfo(Guid? userId )
        {
            if (userId == null)
            {
                var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
                userId = Guid.Parse(userIdStr);
            }
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await userSocialRelationshipBusiness.GetUserRelationShipInfo(userId.Value );
            });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> SubscribUser(Guid connectorUserId, bool isSubscibe)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var owerUserId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await userSocialRelationshipBusiness.SetUserRelationship(owerUserId, connectorUserId, isSubscibe);
            });
            return Json(apiMsg);
        }


        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> GetSubscribByUser(Guid? userId, int skipCount = 0, int pageSize = 10)
        {
            if (userId == null)
            {
                var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
                userId = Guid.Parse(userIdStr);
            }
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await userSocialRelationshipBusiness.GetRelationShipInfoFromUser(userId.Value, skipCount, pageSize);
            });
            return Json(apiMsg); 
        }

        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> GetFansByUser(Guid? userId, int skipCount = 0, int pageSize = 10)
        {
            if (userId == null)
            {
                var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
                userId = Guid.Parse(userIdStr);
            }
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await userSocialRelationshipBusiness.GetRelationShipInfoToUser(userId.Value, skipCount, pageSize);
            });
            return Json(apiMsg);
        }
    }
}