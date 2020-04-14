using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.DataPlatform.Model;
using Oryx.DataPlatForm.Business;
using Oryx.ViewModel;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class DataQueryController : Controller
    {
        private readonly DataOperation DataOperation;
        public DataQueryController(DataOperation dataOperation)
        {
            DataOperation = dataOperation;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStoreMapInfoByDistrict(string district)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await DataOperation.GetStoreMapInfo(district);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetStoreMapInfo()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await DataOperation.GetStoreMapInfo();
            });
            return Json(apiMsg);
        }
    }
}