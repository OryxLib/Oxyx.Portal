using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Content.Business;
using Oryx.Social.Business;

namespace Oryx.SNS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        private readonly BannersBusienss bannersBusienss;
        private readonly PostEntryBusiness postEntryBusiness;
        public CategoryController(ContentBusiness _contentBusiness,
            BannersBusienss _bannersBusienss,
            PostEntryBusiness _postEntryBusiness)
        {
            contentBusiness = _contentBusiness;
            bannersBusienss = _bannersBusienss;
            postEntryBusiness = _postEntryBusiness;
        }
 
    }
}