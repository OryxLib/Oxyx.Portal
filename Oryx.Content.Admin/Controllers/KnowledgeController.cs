using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.ViewModel;

namespace Oryx.Content.Admin.Controllers
{
    public class KnowledgeController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        public KnowledgeController(ContentBusiness _contentBusiness)
        {
            contentBusiness = _contentBusiness;
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
            var dbSet = contentBusiness.ContentAccessor.All<ContentEntry>();
            totalResultsCount = dbSet.Count();
            IQueryable<ContentEntry> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => (x.Title.Contains(model.search.value)
                || x.Content.Contains(model.search.value)) && x.Tags.Any(c => c.Name == KnowledgeBusiness.PublishKey));
            }
            else
            {
                dbQuery = dbSet.Where(x => x.Tags.Any(c => c.Name == KnowledgeBusiness.PublishKey));
            }
            filteredResultsCount = dbQuery.Count();
            var dataList = dbQuery.Include("MediaResource").OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

            return Json(new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(Guid? Id)
        {
            ViewData["DataType"] = typeof(ContentEntry);
            ViewData["DataCategory"] = await contentBusiness.GetCategoryFormByP(KnowledgeBusiness.PublishKey);
            if (Id != null)
            {
                var contentEntry = await contentBusiness.GetContentById(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(ContentEntry contentEntry)
        {
            ViewData["DataType"] = typeof(ContentEntry);
            ViewData["DataCategory"] = await contentBusiness.GetCategoryFormByP(KnowledgeBusiness.PublishKey);
            await contentBusiness.CreateContentWithTag(contentEntry, KnowledgeBusiness.PublishKey);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var contentEntry = await contentBusiness.ContentAccessor.OneAsync<ContentEntry>(x => x.Id == Id, "MediaResource", "Comments", "ContentEntryInfo", "Tags");
                await contentBusiness.ContentAccessor.Delete<ContentEntry>(contentEntry);
            });

            return Json(apiMsg);
        }
    }
}