using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;

namespace Oryx.DataPlatform.Portal.Pages.ContentManager.Banners
{
    public class IndexModel : PageModel
    {
        private BannersBusienss bannerBusiness;
        public IndexModel(BannersBusienss _bannerBusiness)
        {
            bannerBusiness = _bannerBusiness;
        }

        public List<Oryx.Content.Model.Banners> BannerList = new List<Oryx.Content.Model.Banners>();

        public async Task OnGet()
        {
            var result = await bannerBusiness.GetList();
            BannerList = result.ToList();
        }
    }
}