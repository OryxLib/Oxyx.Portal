using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Oryx.SNS.Web.Filters;
using Oryx.SNS.Web.Filters.Attributes;
using Oryx.UserAccount.Business;
using Oryx.Utilities.Redis;
using Oryx.Utilities.SentryIO;
using Oryx.ViewModel;
using Oryx.ViewModel.CommonAccountUser;
using Oryxl.Wx.WebApp;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IDistributedCache distributedCache;
        private readonly UserAccountBusiness userAccountBusiness;
        private readonly IConfiguration configuration;
        //private readonly SinaWeiboClient sinaWeiboClient;
        const string WeiboAppKey = "3303196495";
        const string WeiboAppSecret = "ca4af10e466202035639eeea7300de60";

        private string WxAppId = string.Empty;
        private string WxAppSecret = string.Empty;
        public AuthController(IDistributedCache _distributedCache,
            IConfiguration _configuration,
            //SinaWeiboClient _sinaWeiboClient,
            UserAccountBusiness _userAccountBusiness)
        {
            distributedCache = _distributedCache;
            userAccountBusiness = _userAccountBusiness;
            configuration = _configuration;
            WxAppId = _configuration["Ciyuanya:Wx:WebAppId"];
            WxAppSecret = _configuration["Ciyuanya:Wx:WebSecret"];
            //sinaWeiboClient = _sinaWeiboClient;
        }

        /// <summary>
        /// 验证用户token是否过期,
        /// 没过期则直接返回,
        /// 过期则返回空
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public IActionResult ApiHasLogin()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var apiMsg = new ApiMessage();
            if (string.IsNullOrEmpty(userId))
            {
                apiMsg.Success = false;
                apiMsg.Data = string.Empty;
            }
            else
            {
                apiMsg.Data = Request.Headers["AccessToken"];
            }

            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> ApiLogin([FromBody]CaUserLoginModel caUserLoginModel)
        {
            var apiMsg = new ApiMessage();
            try
            {
                var resultTuple = await userAccountBusiness.LoginUserNamePwd(caUserLoginModel);
                if (resultTuple.Item1)
                {
                    var accessToken = Guid.NewGuid().ToString();
                    var redisDoc = new RedisDocument<string>();
                    redisDoc.ExpireTime = DateTime.Now.AddDays(30);
                    redisDoc.SetTime = DateTime.Now;
                    redisDoc.Value = resultTuple.Item3;
                    redisDoc.Key = accessToken;
                    await distributedCache.SetValue(redisDoc);
                    apiMsg.Data = accessToken;
                }
                else
                {
                    apiMsg.SetFault(resultTuple.Item2);
                }
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }
            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> ApiRegister([FromBody]CaRegisterModel model)
        {
            var apiMsg = new ApiMessage();
            try
            {
                var resultTuple = await userAccountBusiness.RegisterUserNamePwd(model);
                if (resultTuple.Item1)
                {
                    var accessToken = Guid.NewGuid().ToString();
                    var redisDoc = new RedisDocument<string>();
                    redisDoc.ExpireTime = DateTime.Now.AddDays(30);
                    redisDoc.SetTime = DateTime.Now;
                    redisDoc.Value = resultTuple.Item3;
                    redisDoc.Key = accessToken;
                    await distributedCache.SetValue(redisDoc);
                    apiMsg.Data = accessToken;
                }
                else
                {
                    apiMsg.SetFault(resultTuple.Item2);
                }
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }
            return Json(apiMsg);
        }


        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> ApiUserInfo()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var _userId = Guid.Parse(userId);
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await userAccountBusiness.GetUserInfo(_userId);
            });

            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> ApiLogout()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {

            });
            return Json(apiMsg);
        }

        [HttpPost]
        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> ApiChangePassword(string newPassword)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                userAccountBusiness.ChangePassword(userId, newPassword);
            });
            return Json(apiMsg);
        }

        [HttpPost]
        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> ApiCheckOldPassword(string oldPassword)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                userAccountBusiness.CheckOldPassword(userId, oldPassword);
            });
            return Json(apiMsg);
        }


        public async Task<IActionResult> WxLogin(string code, string url, string state)
        {
            var accessTokenResult = await WebAppAutherize.GetUserAccessToken(WxAppId, WxAppSecret, code);
            await distributedCache.SetValue(new RedisDocument<UserAccessTokenResult>
            {
                ExpireTime = DateTime.Now.AddSeconds(accessTokenResult.ExpiresIn),
                Key = WxAppId + "_" + accessTokenResult.OpenId + "_AccessToken",
                SetTime = DateTime.Now,
                Value = accessTokenResult
            });
            var userInfo = await WebAppAutherize.GetUserInfo(accessTokenResult.AccessToken, accessTokenResult.OpenId);
            userInfo.AppId = configuration["Ciyuanya:Wx:WebAppId"];
            var wxLoginUserId = await userAccountBusiness.WxWebLoin(userInfo);
            //将用户Id提交到Redis 缓存

            HttpContext.Session.Set(UserAccountBusiness.UserAccountSessionkey, Encoding.UTF8.GetBytes(wxLoginUserId));
            await HttpContext.Session.CommitAsync();
            await SetAuth(wxLoginUserId);

            return Redirect(url);
        }

        public async Task<IActionResult> HasLogin()
        {
            await HttpContext.Session.LoadAsync();
            byte[] sessionValueBytes;
            var apiMsg = new ApiMessage();
            if (HttpContext.Session.TryGetValue(UserAccountBusiness.UserAccountSessionkey, out sessionValueBytes))
            {
                var sessionValue = Encoding.UTF8.GetString(sessionValueBytes);
                if (!string.IsNullOrEmpty(sessionValue))
                {
                    apiMsg.Data = sessionValue;
                }
                else
                {
                    apiMsg.SetFault("Can not find UserAccount");
                }
            }
            else
            {
                apiMsg.SetFault("Can not find UserAccount");
            }

            return Json(apiMsg);
        }

        [HttpPost]
        [PageIgnore]
        public async Task<IActionResult> CreateUser([FromBody]CaRegisterModel model)
        {
            ApiMessage apiMsg;
            if (model.InviteOrigin != "LnnInvite")
            {
                apiMsg = new ApiMessage();
                apiMsg.SetFault("邀请码不能为空");
            }
            else
            {
                apiMsg = await ApiMessage.Wrap(async () =>
                {
                    var resultTuple = await userAccountBusiness.RegisterUserNamePwd(model);
                    HttpContext.Session.Set
                        (UserAccountBusiness.UserAccountSessionkey,
                        Encoding.UTF8.GetBytes(resultTuple.Item3));
                    await HttpContext.Session.CommitAsync();
                    await SetAuth(resultTuple.Item3);
                    return resultTuple.Item3;
                });
            }


            return Json(apiMsg);

        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]CaUserLoginModel caUserLoginModel)
        {
            var resultTuple = await userAccountBusiness.LoginUserNamePwd(caUserLoginModel);
            var apiMsg = new ApiMessage();
            if (resultTuple.Item1)
            {
                apiMsg.Message = resultTuple.Item2;
                HttpContext.Session.Set
                    (UserAccountBusiness.UserAccountSessionkey,
                    Encoding.UTF8.GetBytes(resultTuple.Item3));
                await HttpContext.Session.CommitAsync();
                await SetAuth(resultTuple.Item3);
                apiMsg.Data = resultTuple.Item3;
            }
            else
            {
                apiMsg.SetFault(resultTuple.Item2);
            }
            return Json(apiMsg);
        }

        private async Task SetAuth(string userId)
        {
            var accessToken = Guid.NewGuid().ToString();
            await distributedCache.SetStringAsync(accessToken, userId);
            HttpContext.Response.Headers["AccessToken"] = accessToken;
        }

        public async Task<IActionResult> WeiboLogin(string code)
        {
            SentryLog.Log("记录 微博登录 回调值:" + code);
            return Content(code);
        }

        public async Task<IActionResult> WeiboLogout(string code)
        {
            SentryLog.Log("记录 微博取消登录 回调值:" + code);
            return Content(code);
        }
    }

}