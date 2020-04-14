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
    public class CartoonManagerController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        public CartoonManagerController(ContentBusiness _contentBusiness)
        {
            contentBusiness = _contentBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Empty()
        {
            return View();
        }
        public async Task<IActionResult> BookDetail(Guid Id)
        {
            ViewData["DataModel"] = await contentBusiness.GetContentListByCategoryId(Id);
            return View();
        }

        public async Task<IActionResult> BookPageDetail(Guid Id)
        {
            var content = await contentBusiness.GetContentById(Id);
            ViewData["DataModel"] = content.MediaResource.OrderBy(x => x.Order).Select(x => x.ActualPath).ToList();
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
            filteredResultsCount = dbQuery.Where(x => x.MediaResource.Count > 0 && x.Status == ContentStatus.Normal).Count();
            var dataList = dbQuery.Include("MediaResource").Where(x => x.MediaResource.Count > 0 && x.Status == ContentStatus.Normal).OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }

        public IActionResult EmptyTable(DataTableAjaxPostModel model)
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
            filteredResultsCount = dbQuery.Where(x => x.ContentList.Count == 0 && x.Status == ContentStatus.Normal).Count();
            var dataList = dbQuery.Include("MediaResource").Where(x => x.ContentList.Count == 0 && x.Status == ContentStatus.Normal).OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

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
            var pcat = await contentBusiness.GetCategoryByName("漫画");
            categories.ParentCategory = pcat;
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