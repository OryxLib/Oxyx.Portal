using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.DataPlatform.Portal.Filters;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.Social.Model;
using Oryx.UserAccount.Business;
using Oryx.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class TopicController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        private readonly UserAccountBusiness userAccountBusiness;
        public TopicController(ContentBusiness _contentBusiness, UserAccountBusiness _userAccountBusiness)
        {
            contentBusiness = _contentBusiness;
            userAccountBusiness = _userAccountBusiness;
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
            var dbSet = contentBusiness.ContentAccessor.All<PostEntryTopic>();//.Where(x => x.Category.Name == "论坛" & !x.IsFaq);
            totalResultsCount = dbSet.Count();
            IQueryable<PostEntryTopic> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.Text.Contains(model.search.value));
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
            ViewData["DataType"] = typeof(ContentEntry);
            if (Id != null)
            {
                var contentEntry = await contentBusiness.GetContentById(Id.Value);
                ViewData["DataModel"] = contentEntry;
            }

            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(PageAuthentication))]
        public async Task<IActionResult> AddOrUpdate(ContentEntry contentEntry)
        {
            var UserId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            var _userId = Guid.Parse(UserId);
            if (contentEntry.ContentEntryInfo == null)
            {
                var userInfo = await userAccountBusiness.GetUserInfo(_userId);
                contentEntry.ContentEntryInfo = new Oryx.Content.Model.ContentEntryExtension.ContentEntryInfo
                {
                    Author = userInfo.nickName,
                    Id = Guid.NewGuid(),
                    Type = "",
                    UserAccountId = _userId
                };
            }
            ViewData["DataType"] = typeof(ContentEntry);
            await contentBusiness.CreateContentWithCategoryAndTag(contentEntry, "论坛", "论坛");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await contentBusiness.ContentAccessor.Delete<PostEntryTopic>(x => x.Id == Id);
            });

            return Json(apiMsg);
        }
    }
}