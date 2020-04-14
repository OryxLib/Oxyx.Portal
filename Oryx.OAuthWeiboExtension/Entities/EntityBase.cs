using System;
using System.Collections.Generic;
#if !NET20
using System.Linq;
#endif
using System.Text;
using Oryx.OAuthWeiboExtension.Json;

namespace Oryx.OAuthWeiboExtension.Weibo.Entities
{
	/// <summary>
	/// EntityBase
	/// </summary>
	public abstract class EntityBase
	{
		/// <summary>
		/// 返回对象原始Json字符串
		/// </summary>
		/// <returns>json</returns>
		public override string ToString()
		{
			//return base.ToString();

			return Oryx.OAuthWeiboExtension.Json.JsonConvert.SerializeObject(this);
		}
	}
}
