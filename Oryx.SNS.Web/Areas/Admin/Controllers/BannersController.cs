using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly BannersBusienss bannersBusienss;

        public BannersController(BannersBusienss _bannersBusienss)
        {
            bannersBusienss = _bannersBusienss;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            //var bannerList = await bannersBusienss.GetList();

            return View("/Pages/ContentManager/Banners/Index.cshtml");
            //return View();
        }

        //public IActionResult New()
        //{
        //    return View();
        //}
        [HttpPost] 
        public async Task<IActionResult> New( Banners model)
        {
            var apiMsg = new ApiMessage();
            try
            {
                await bannersBusienss.Add(model);
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }
            return Json(apiMsg);
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var data = await ApiMessage.WrapData(async () =>
            {
                return await bannersBusienss.GetOne(Id);
            });

            return View(data);
        }

        public async Task<IActionResult> Update(Banners model)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await bannersBusienss.Update(model);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = new ApiMessage();
            try
            {
                await bannersBusienss.Delete(Id);
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }

            //var apiMsg = await ApiMessage.Wrap(async () =>
            //{
            //    await bannersBusienss.Delete(Id);
            //});
            return Json(apiMsg);
        }
    }
}
