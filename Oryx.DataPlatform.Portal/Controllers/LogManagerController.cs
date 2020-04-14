using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Content.Business;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.Model.UserAccountExtend;
using Oryx.ViewModel;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class LogManagerController : Controller
    {
        private readonly UserAccountBusiness userAccountBusiness;
        public LogManagerController(UserAccountBusiness _userAccountBusiness)
        {
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
            var dbSet = userAccountBusiness.userAccountAccessor.All<UserAccountLoginLog>();
            totalResultsCount = dbSet.Count();
            IQueryable<UserAccountLoginLog> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.UserAccountEntry.NickName.Contains(model.search.value)
                || x.UserAccountEntry.UserNamePwd.UserName.Contains(model.search.value)
                || x.IP.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = dbQuery.Count();
            var dataList = dbQuery.Include("UserAccountEntry").OrderByDescending(x => x.CreateTime).Skip(model.start).Take(model.length).ToList();

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = dataList
            });
        }
    }
}