using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.favorite
{
	public class IDCollection : EntityBase
	{
		[JsonProperty("favorites")]
		public IEnumerable<IDEntity> Favorites { get; internal set; }
		[JsonProperty("total_number")]
		public int TotalNumber { get; internal set; }
	}
}
