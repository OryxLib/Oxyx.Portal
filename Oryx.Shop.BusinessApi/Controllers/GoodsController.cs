using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Shop.Business.Bs.Goods;
using Oryx.Shop.BusinessApi.Filters;
using Oryx.Shop.Model.Shop.Goods;
using Oryx.ViewModel;

namespace Oryx.Shop.BusinessApi.Controllers
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
        public async Task<IActionResult> GetList(int? categoryId, int skipCount, int pageSize = 10)
        {
            ApiMessage result;
            if (categoryId != null)
            {
                result = await goodsBs.ListGoods(categoryId.Value, skipCount, pageSize);
            }
            else
            {
                result = await goodsBs.ListGoods(0, skipCount, pageSize);
            }
            return Json(result);
        }

        [TypeFilter(typeof(AdminUserAuthenticationFilter))]
        public async Task<IActionResult> GetGoodInfo(Guid Id)
        {
            var result = await goodsBs.GetInfo(Id);
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