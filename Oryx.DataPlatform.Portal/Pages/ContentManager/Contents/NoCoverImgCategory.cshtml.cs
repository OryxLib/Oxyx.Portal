using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business; 

namespace Oryx.DataPlatform.Portal.Pages.ContentManager.Contents
{
    public class NoCoverImgCategoryModel : PageModel
    {
        private ContentBusiness contentBusiness;
        public NoCoverImgCategoryModel(ContentBusiness _bannerBusiness)
        {
            contentBusiness = _bannerBusiness;
        }

        public List<Oryx.Content.Model.Categories> CategoryList = new List<Oryx.Content.Model.Categories>();

        public async Task OnGet(string query, int skipCount = 0, int pageSize = 20)
        {
            var result = await contentBusiness.GetTopCategory("");
            CategoryList = result.ToList();
        }
    }
}