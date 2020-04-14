using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.favorite
{
	public class IDEntity : EntityBase
	{
		[JsonProperty("status")]
		public string Status { get; internal set; }

		[JsonProperty("tags")]
		public IEnumerable<TagEntity> Tags { get; internal set; }

		[JsonProperty("favorited_time")]
		public string FavoritedTime { get; internal set; }

	}
}
