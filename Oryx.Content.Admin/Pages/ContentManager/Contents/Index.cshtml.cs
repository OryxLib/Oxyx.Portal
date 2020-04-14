using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;

namespace Oryx.Content.Admin.Pages.ContentManager.Contents
{
    public class IndexModel : PageModel
    {
        private ContentBusiness contentBusiness;
        public IndexModel(ContentBusiness _bannerBusiness)
        {
            contentBusiness = _bannerBusiness;
        }

        public List<Model.Categories> CategoryList = new List<Model.Categories>();

        public async Task OnGet(int skipCount = 0, int pageSize = 20)
        {
            //var result = await contentBusiness.GetNoCoverPage(skipCount, pageSize);
            //CategoryList = result.ToList();
        }
    }
}