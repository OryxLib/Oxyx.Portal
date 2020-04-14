using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo
{
	public class AccessToken
	{
		[JsonProperty(PropertyName = "access_token")]
		public string Token{get;internal set;}
		[JsonProperty(PropertyName = "expires_in")]
		public int ExpiresIn{get;internal set;}
		[JsonProperty(PropertyName = "uid")]
		public string UID{get;internal set;}
		[JsonProperty(PropertyName = "refresh_token")]
		public string RefreshToken { get; internal set; }

	}
}
