using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.ViewModel;
using Oryxl.DynamicPage.Business;
using Oryxl.DynamicPage.Model;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class DynamicRouteController : Controller
    {
        private readonly DynamicPageBusiness dynamicPageBusiness;

        public DynamicRouteController(DynamicPageBusiness _dynamicPageBusiness)
        {
            dynamicPageBusiness = _dynamicPageBusiness;
        }

        public IActionResult RouteList()
        {
            return View();
        }

        public async Task<IActionResult> ApiRouteList(DataTableAjaxPostModel model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var dbSet = dynamicPageBusiness.routeAccessor.All<RouteEntry>();
            totalResultsCount = dbSet.Count();
            IQueryable<RouteEntry> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Name.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = await dbQuery.CountAsync();
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

        public async Task<IActionResult> ApiRouteJSON()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await dynamicPageBusiness.routeAccessor.All<RouteEntry>().Where(x => x.ParentRoute == null).ToListAsync();
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> RouteGet(Guid Id)
        {
            var routeModel = await dynamicPageBusiness.routeAccessor.OneAsync<RouteEntry>(x => x.Id == Id);

            return View(routeModel);
        }

        public IActionResult RouteAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RouteAdd(RouteEntry route)
        {
            await dynamicPageBusiness.routeAccessor.Add(route);

            return RedirectToAction("RouteList");
        }

        public async Task<IActionResult> RouteEdit(Guid Id)
        {
            var routeModel = await dynamicPageBusiness.routeAccessor.OneAsync<RouteEntry>(x => x.Id == Id);

            return View(routeModel);
        }

        [HttpPost]
        public async Task<IActionResult> RouteEdit(RouteEntry route)
        {
            await dynamicPageBusiness.routeAccessor.Update(route);
            return RedirectToAction("RouteList");
        }

        public async Task<IActionResult> RouteDelete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
             {
                 await dynamicPageBusiness.routeAccessor.Delete<RouteEntry>(x => x.Id == Id);
             });

            return Json(apiMsg);
        }

        public IActionResult PageList()
        {
            return View();
        }

        public async Task<IActionResult> ApiPageList(DataTableAjaxPostModel model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var dbSet = dynamicPageBusiness.routeAccessor.All<ReponseEntry>();
            totalResultsCount = dbSet.Count();
            IQueryable<ReponseEntry> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Title.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = await dbQuery.CountAsync();
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

        public async Task<IActionResult> ApiPageJSON()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await dynamicPageBusiness.routeAccessor.All<ReponseEntry>().Where(x => x.IsMaster).ToListAsync();
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> PageGet(Guid Id)
        {
            var routeModel = await dynamicPageBusiness.routeAccessor.OneAsync<ReponseEntry>(x => x.Id == Id);

            return View(routeModel);
        }

        public ActionResult PageAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PageAdd(ReponseEntry page)
        {
            await dynamicPageBusiness.routeAccessor.Add(page);

            return RedirectToAction("PageList");
        }

        public async Task<IActionResult> PageEdit(Guid Id)
        {
            var routeModel = await dynamicPageBusiness.routeAccessor.OneAsync<ReponseEntry>(x => x.Id == Id, "Route");

            return View(routeModel);
        }
        [HttpPost]
        public async Task<IActionResult> PageEdit(ReponseEntry page)
        {
            await dynamicPageBusiness.routeAccessor.Update(page);
            return RedirectToAction("PageList");
        }

        public async Task<IActionResult> PageDelete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await dynamicPageBusiness.routeAccessor.Delete<ReponseEntry>(x => x.Id == Id);
            });

            return Json(apiMsg);
        }
    }
}