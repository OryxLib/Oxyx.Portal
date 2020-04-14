using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.Model;
using Oryx.UserAccount.Model.UserBusinessExtend;
using Oryx.ViewModel;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserAccountBusiness userAccountBusiness;
        public UserAccountController(UserAccountBusiness _userAccountBusiness)
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
            var dbSet = userAccountBusiness.userAccountAccessor.All<UserAccountEntry>();
            totalResultsCount = dbSet.Count();
            IQueryable<UserAccountEntry> dbQuery;
            if (model?.search?.value != null)
            {
                dbQuery = dbSet.Where(x => x.NickName.Contains(model.search.value)
                || x.UserNamePwd.UserName.Contains(model.search.value));
            }
            else
            {
                dbQuery = dbSet;
            }
            filteredResultsCount = dbQuery.Count();
            dbQuery = dbQuery.Include(x => x.Phone).Include(x => x.UserNamePwd).Include(x => x.Roles);
            var dataList = dbQuery.OrderByDescending(x => x.CreateTime).Select(x => new
            {
                x.Id,
                x.NickName,
                x.UserNamePwd.UserName,
                x.Phone.Phone,
                x.Roles
            }).Skip(model.start).Take(model.length).ToList();

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
            ViewData["DataType"] = typeof(UserAccountEntry);
            if (Id != null)
            {
                var userAccountEntry = await userAccountBusiness.GetUserAccountEntry(Id.Value);
                ViewData["DataModel"] = userAccountEntry;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(UserAccountEntry userAccountEntry)
        {
            ViewData["DataType"] = typeof(UserAccountEntry);
            await userAccountBusiness.AddOrUpdateUser(userAccountEntry, UserAccountBusiness.GeneralUserRoleKey);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(Guid Id)
        {
            ViewData["DataType"] = typeof(UserAccountBusinessRole);
            if (Id != null)
            {
                var userAccountRole = await userAccountBusiness.GetRoles(Id.ToString());
                ViewData["DataModel"] = userAccountRole;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(Guid Id, string roleName)
        {
            ViewData["DataType"] = typeof(UserAccountBusinessRole);
            await userAccountBusiness.ChangeRole(Id, roleName);
            return Redirect("/UserAccount/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await userAccountBusiness.DeleteUser(Id);
            });

            return Json(apiMsg);
        }
    }
}