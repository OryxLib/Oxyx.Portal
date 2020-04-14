using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.CommonDbOperation;
using Oryx.CommonInterface.Interface;
using Oryx.ViewModel;
using Oryx.ViewModel.CRUD;

namespace Oryx.SNS.Web.BaseControllers
{
    public class BaseController<T> : Controller
        where T : class, IEntityModel, new()
    {
        public CommonAccessor accessor;
        public BaseController(CommonAccessor _accessor)
        {
            accessor = _accessor;
        }

        public async Task<IActionResult> JsonGet(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await accessor.OneAsync<T>(x => x.Id == Id);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> JsonList(ListQuery listQuery)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await accessor.ListAsync<T>();
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> JsonAdd(T model)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await accessor.Add(model);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> JsonEdit(T model)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await accessor.Update(model);
            });
            return Json(apiMsg);
        }


        public async Task<IActionResult> JsonDelete(T model)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await accessor.Delete<T>(x => x.Id == model.Id);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> ViewList(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var dbSet = accessor.All<T>();
            totalResultsCount = await dbSet.CountAsync();
            IQueryable<T> dbQuery;
            //if (model?.search?.value != null)
            //{
            //    dbQuery = dbSet.Where(x => x.Name.Contains(model.search.value) || x.Description.Contains(model.search.value));
            //}
            //else
            //{
            dbQuery = dbSet;
            //}
            filteredResultsCount = dbQuery.Count();
            var dataList = await dbQuery.OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToListAsync();

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }

        public async Task<IActionResult> ViewGet(Guid Id)
        {
            ViewData["DataModel"] = await accessor.OneAsync<T>(x => x.Id == Id);
            return View("~/BaseControllers/Views/BaseGet.cshtml");
        }

        public IActionResult ViewAdd()
        {
            ViewData["DataType"] = typeof(T);
            return View("~/BaseControllers/Views/BaseGet.cshtml");
        }
    }
}