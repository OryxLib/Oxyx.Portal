using System;
using System.Collections.Generic;
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities 
{
	public class GeoEntity: EntityBase
	{
		[JsonProperty("type")]	
		public string Type { get; internal set; }
		[JsonProperty("coordinates")]	
		public IEnumerable<float> Coordinates { get; internal set; }
	}
}
