using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;
namespace Oryx.OAuthWeiboExtension.Weibo.Entities.shortUrl
{
	public class CommentComments : EntityBase
	{
		[JsonProperty("url_short")]
		public string ShortUrl { get; internal set; }
		[JsonProperty("url_long")]
		public string LongUrl { get; internal set; }
		[JsonProperty("share_comments")]
		public IEnumerable<Entities.comment.Entity> Referers { get; internal set; }

	}
}
