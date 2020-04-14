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
    public class CategoryController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        public CategoryController(ContentBusiness _contentBusiness)
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
            var dbSet = contentBusiness.CategoryAccessor.All<Categories>();
            totalResultsCount = dbSet.Count();
            IQueryable<Categories> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Name.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = dbQuery.Where(x =>
            x.ParentCategory.Name != "漫画" &&
            x.Name != "漫画" &&
             x.Status == ContentStatus.Normal).Count();

            var dataList = dbQuery.Include("MediaResource").Where(x =>
            x.ParentCategory.Name != "漫画" &&
            x.Name != "漫画" &&
            x.Status == ContentStatus.Normal
            ).OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

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
                var contentEntry = await contentBusiness.GetCategoryById(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Categories categories)
        {
            ViewData["DataType"] = typeof(FileEntry);
            var _fileEntry = await contentBusiness.CreateCategory(categories);
            ViewData["DataModel"] = _fileEntry;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {

            });

            return Json(apiMsg);
        }
    }
}