﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace Oryx.OAuthWeiboExtension.Weibo.Interface.Dynamic
{
	/// <summary>
	/// Search接口
	/// </summary>
	public class SearchInterface: WeiboInterface
	{
		SearchAPI api;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="client">操作类</param>
		public SearchInterface(Client client)
			: base(client)
		{
			api = new SearchAPI(client);
		}
		/// <summary>
		/// 搜索用户时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public dynamic Users(string q, int count = 10)
		{
			return DynamicJson.Parse(api.Users(q,count));
		}
		/// <summary>
		/// 搜索微博时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public dynamic Statuses(string q, int count = 10)
		{
			return DynamicJson.Parse(api.Statuses(q,count));
		}
		/// <summary>
		/// 搜索学校时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10。 </param>
		/// <param name="type">学校类型，0：全部、1：大学、2：高中、3：中专技校、4：初中、5：小学，默认为0。 </param>
		/// <returns></returns>
		public dynamic Schools(string q, int count = 10,int type=0)
		{
			return DynamicJson.Parse(api.Schools(q,count,type));
		}
		/// <summary>
		/// 搜索公司时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public dynamic Companies(string q, int count = 10)
		{
			return DynamicJson.Parse(api.Companies(q,count));
		}
		/// <summary>
		/// 搜索应用时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public dynamic Apps(string q, int count = 10)
		{
			return DynamicJson.Parse(api.Apps(q,count));
		}
		/// <summary>
		/// @用户时的联想建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10，粉丝最多1000，关注最多2000。 </param>
		/// <param name="type">联想类型，0：关注、1：粉丝。</param>
		/// <param name="range">联想范围，0：只联想关注人、1：只联想关注人的备注、2：全部，默认为2。</param>
		/// <returns></returns>
		public dynamic AtUsers(string q, int count = 10, int type = 0,int range=2)
		{
			return DynamicJson.Parse(api.AtUsers(q,count,type,range));
		}
		/// <summary>
		/// 搜索某一话题下的微博 
		/// </summary>
		/// <param name="q">搜索的话题关键字</param>
		/// <param name="count">单页返回的记录条数，默认为10，最大为50。 </param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public dynamic Topics(string q, int count = 10,int page=1)
		{
			return DynamicJson.Parse(api.Topics(q,count,page));
		}


	}
}
