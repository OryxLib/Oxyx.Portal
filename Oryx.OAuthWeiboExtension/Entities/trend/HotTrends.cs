﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities.trend
{
	public class HotTrends : EntityBase
	{
		public Dictionary<string, List<Keyword>> Trends { get; internal set; }
		public string AsOf { get; set; }
	}
}
