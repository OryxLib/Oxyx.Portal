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
    public class FileManagerController : Controller
    {
        private readonly FileEntryBusiness fileEntryBusiness;
        public FileManagerController(FileEntryBusiness _fileEntryBusiness)
        {
            fileEntryBusiness = _fileEntryBusiness;
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
            var dbSet = fileEntryBusiness.dataFileEntryAccessor.All<FileEntry>().Where(x => x.Tag == FileEntryBusiness.PublishKey);
            totalResultsCount = dbSet.Count();
            IQueryable<FileEntry> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Name.Contains(model.search.value));
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
            ViewData["DataType"] = typeof(FileEntry);
            if (Id != null)
            {
                var contentEntry = await fileEntryBusiness.GetFileEntry(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(FileEntry fileEntry)
        {
            ViewData["DataType"] = typeof(FileEntry);
            var _fileEntry = await fileEntryBusiness.AddOrUpdateFileEntry(fileEntry);
            ViewData["DataModel"] = _fileEntry;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await fileEntryBusiness.DeleteFile(Id);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> Upload()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var files = Request.Form.Files;

                return await fileEntryBusiness.SaveLocal(files, "文件管理", "");
            });
            return Json(apiMsg);
        }
    }
}