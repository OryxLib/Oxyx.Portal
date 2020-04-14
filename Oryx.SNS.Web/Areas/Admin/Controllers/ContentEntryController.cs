using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.ViewModel;
using Oryx.ViewModel.CRUD;
using Oryx.ViewModel.DataTableViewModel;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class ContentEntryController : Controller
    {
        private ContentBusiness contentBusiness;
        public ContentEntryController(ContentBusiness _bannerBusiness)
        {
            contentBusiness = _bannerBusiness;
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
                dbQuery = dbSet.Where(x => x.Name.Contains(model.search.value) || x.Description.Contains(model.search.value));
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

        public IActionResult NoCoverTable(DataTableAjaxPostModel model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var dbSet = contentBusiness.CategoryAccessor.All<Categories>().Where(x => x.ParentCategory == null && (x.MediaResource == null || x.MediaResource.Count < 1));
            totalResultsCount = dbSet.Count();
            filteredResultsCount = dbSet.Count();
            var dataList = dbSet.Include("MediaResource").OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }


        public IActionResult ContentListTable(DataTableWithPid model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var dbSet = contentBusiness.CategoryAccessor.All<ContentEntry>();
            totalResultsCount = dbSet.Count();
            IQueryable<ContentEntry> dbQuery;
            if (model.pid != null)
            {
                dbQuery = dbSet.Where(x => x.Category.Id == model.pid);
            }
            else
            {
                dbQuery = dbSet;
            }
            if (model?.search?.value != null)
            {
                dbQuery = dbQuery.Where(x => x.Title.Contains(model.search.value) || x.Content.Contains(model.search.value));
            }

            filteredResultsCount = dbQuery.Count();
            var dataList = dbQuery.OrderBy(x => x.Order).Skip(model.start).Take(model.length).ToList();

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }

        public async Task<IActionResult> ListCategory(ListQuery listQuery)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await contentBusiness.GetCategory(listQuery);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> AddCategory(Categories category)
        {
            var apiMsg = await contentBusiness.CreateCategory(category);
            return Json(apiMsg);
        }

        public async Task<IActionResult> AddContent(ContentEntry content)
        {
            var apiMsg = await contentBusiness.CreateContent(content);
            return Json(apiMsg);
        }

        public async Task<IActionResult> AddMultiContents(Guid pid, string contentName, int count)
        {
            var apiMsg = await ApiMessage.Wrap(async () => await contentBusiness.CreateMultiContent(pid, contentName, count));
            return Json(apiMsg);
        }

        public async Task<IActionResult> AddContentMediaResource(Guid contentId, List<FileEntry> fileEntryList)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await contentBusiness.AppenContentMediaResource(contentId, fileEntryList);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> SetCloseCategory(Guid categoryId)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await contentBusiness.SetCategoryStatus(categoryId, ContentStatus.Close);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> DeleteMediaResource(Guid contentId, Guid fileEntryId)
        {
            return Json(new { });
        }

        public async Task<IActionResult> DeleteContent(Guid contentId)
        {
            return Json(new { });
        }

        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            return Json(new { });
        }
    }
}