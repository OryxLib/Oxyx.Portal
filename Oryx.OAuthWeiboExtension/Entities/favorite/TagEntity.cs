using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.favorite
{
	public class TagEntity : EntityBase
	{
		[JsonProperty("id")]
		public string ID { get; internal set; }
		[JsonProperty("tag")]
		public string Tag { get; internal set; }
		[JsonProperty("count")]
		public int Count { get; internal set; }
	}
}
