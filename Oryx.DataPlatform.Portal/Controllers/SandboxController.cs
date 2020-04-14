using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oryx.Common.Business;
using Oryx.Common.Model;
using Oryx.Common.Model.SandBox;
using Oryx.UserAccount.Business;
using Oryx.ViewModel;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class SandboxController : Controller
    {
        private readonly SandBoxBusiness sandBoxBusiness;

        private readonly TipEntryBussiness tipEntryBussiness;

        public SandboxController(SandBoxBusiness _sandBoxBusiness, TipEntryBussiness _tipEntryBussiness)
        {
            sandBoxBusiness = _sandBoxBusiness;
            tipEntryBussiness = _tipEntryBussiness;
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
            var dbSet = tipEntryBussiness.tipEntryDataAccessor.All<TipEntry>();
            totalResultsCount = dbSet.Count();
            IQueryable<TipEntry> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Title == model.search.value || x.Content == model.search.value);
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
            ViewData["DataType"] = typeof(TipEntry);
            if (Id != null)
            {
                var contentEntry = await tipEntryBussiness.GetTipeId(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(TipEntry tipEntry)
        {
            ViewData["DataType"] = typeof(TipEntry);
            await tipEntryBussiness.CreateOrUpdateTipEntry(tipEntry);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendTip(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var UserId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
                var _userId = Guid.Parse(UserId);
                var tipEntry = await tipEntryBussiness.GetTipeId(Id);
                await sandBoxBusiness.SendAlertToAll(new SandBoxMessage
                {
                    Content = tipEntry.Content,
                    CreateTime = DateTime.Now,
                    FromUserAccountId = _userId
                });
            });
            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await tipEntryBussiness.Delete(Id);
            });

            return Json(apiMsg);
        }
    }
}