using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.user
{
	public class IDCollection : EntityBase
	{
		[JsonProperty("ids")]
		public IEnumerable<string> Users { get; internal set; }
		[JsonProperty(PropertyName = "previous_cursor")]
		public string ProviousCursor { get; internal set; }
		[JsonProperty(PropertyName = "next_cursor")]
		public string NextCursor { get; internal set; }
		[JsonProperty(PropertyName = "total_number")]
		public int TotalNumber { get; internal set; }
	}
}
