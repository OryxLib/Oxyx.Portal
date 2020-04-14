using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.trend
{
	public class Trend : EntityBase
	{
		[JsonProperty("trend_id")]
		public string ID { get; internal set; }
		[JsonProperty("hotword")]
		public string HotWord { get; internal set; }
		[JsonProperty("num")]
		public string Number { get; internal set; }
	}
}
