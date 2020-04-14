using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Shop.Business.Bs.Goods;
using Oryx.Shop.Filters;
using Oryx.Shop.Model.Shop.Goods;

namespace Oryx.Shop.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IDistributedCache distributedCache;
        private readonly GoodsBs goodsBs;
        int pageSize = 10;
        public GoodsController(IDistributedCache _distributedCache, GoodsBs _goodsBs)
        {
            distributedCache = _distributedCache;
            goodsBs = _goodsBs;
        }
        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> UpdateCategory(int id,string name)
        {
            var result = await goodsBs.ListGoodCategory();
            var cate = (result.Data as List<GoodCategory>).FirstOrDefault(x => x.Id == id);
            cate.Name = name;
            var nReesult =await goodsBs.UpdateCategory(cate);
            return Json(nReesult);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> AddGoods(GoodEntity goodModel)
        {
            var result = await goodsBs.AddGood(goodModel);
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> AddGoodCategory(GoodCategory category)
        {
            var result = await goodsBs.AddGoodCategory(category);
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> GetListGoodCategory()
        {
            var result = await goodsBs.ListGoodCategory();
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> GetGoodInfo(Guid Id)
        {
            var result = await goodsBs.GetInfo(Id);
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> GetListGoods(int page = 1)
        {
            var skipPage = pageSize * (page - 1);
            var result = await goodsBs.ListGoods(skipPage, pageSize);
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> UpdateGood(GoodEntity goodEntity)
        {
            var result = await goodsBs.UpdateGood(goodEntity);
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> DeleteGood(Guid Id)
        {
            var result = await goodsBs.DeleteGood(Id);
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public IActionResult Index()
        {
            var existingTime = distributedCache.GetString("test");
            distributedCache.SetStringAsync("test", "good");
            return Json(new { success = true });
        }
    }

}