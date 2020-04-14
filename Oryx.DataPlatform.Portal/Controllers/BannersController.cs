using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class BannersController : Controller
    {
        private readonly BannersBusienss bannersBusienss;

        public BannersController(BannersBusienss _bannersBusienss)
        {
            bannersBusienss = _bannersBusienss;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Table(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var dbSet = bannersBusienss.BannerDataAccessor.All<Banners>();
            totalResultsCount = dbSet.Count();
            IQueryable<Banners> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Title.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = dbQuery.Count();
            var dataList = dbQuery.OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(Guid? Id)
        {
            ViewData["DataType"] = typeof(Banners);
            if (Id != null)
            {
                var contentEntry = await bannersBusienss.GetOne(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Banners banner)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                ViewData["DataType"] = typeof(Banners);
                await bannersBusienss.AddOrUpdate(banner);
            });
            return Json(apiMsg);
            //return RedirectToAction("Index");
        }


        //public IActionResult New()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> New(Oryx.Content.Model.Banners model)
        {
            var apiMsg = new ApiMessage();
            try
            {
                await bannersBusienss.Add(model);
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }
            return Json(apiMsg);
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var data = await ApiMessage.WrapData(async () =>
            {
                return await bannersBusienss.GetOne(Id);
            });

            return View(data);
        }

        public async Task<IActionResult> Update(Oryx.Content.Model.Banners model)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await bannersBusienss.Update(model);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = new ApiMessage();
            try
            {
                await bannersBusienss.Delete(Id);
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }

            //var apiMsg = await ApiMessage.Wrap(async () =>
            //{
            //    await bannersBusienss.Delete(Id);
            //});
            return Json(apiMsg);
        }
    }
}
