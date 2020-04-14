using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.search
{
	public class Status : EntityBase
	{
		[JsonProperty("suggestion")]
		public string Suggestion { get; internal set; }
		[JsonProperty("count")]
		public int Count { get; internal set; }
	}
}
