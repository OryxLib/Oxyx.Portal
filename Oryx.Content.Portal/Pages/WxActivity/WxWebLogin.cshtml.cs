using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Oryx.Content.Portal.Pages.WxActivity
{
    public class WxWebLoginModel : PageModel
    {
        private readonly IConfiguration configuration;

        public string GotoUrl { get; set; }

        public WxWebLoginModel(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public void OnGet(string url, string scope = "snsapi_userinfo")
        {
            GotoUrl = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={configuration["Ciyuanya:Wx:WebAppId"]}&redirect_uri={ WebUtility.UrlEncode(url)}&response_type=code&scope={scope}&state=123#wechat_redirect";
        }
    }
}