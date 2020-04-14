using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.ViewModel;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class KnowledgeCategoryController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        public KnowledgeCategoryController(ContentBusiness _contentBusiness)
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
            var dbSet = contentBusiness.ContentAccessor.All<Categories>().Where(x => x.ParentCategory.Name == "知识库");
            totalResultsCount = dbSet.Count();
            IQueryable<Categories> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Name.Contains(model.search.value)
                || x.Description.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = dbQuery.Count();
            var dataList = dbQuery.Include("MediaResource").OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

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
            ViewData["DataType"] = typeof(Categories);
            if (Id != null)
            {
                var contentEntry = await contentBusiness.GetCategoryForm(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Categories contentEntry)
        {
            ViewData["DataType"] = typeof(Categories);
            await contentBusiness.CreateCategoryWidth(contentEntry, "知识库");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var category = await contentBusiness.ContentAccessor.OneAsync<Categories>(x => x.Id == Id, "ChildrenCategories", "ContentList", "Tags");
                await contentBusiness.ContentAccessor.Delete<Categories>(category);
            });

            return Json(apiMsg);
        }
    }
}