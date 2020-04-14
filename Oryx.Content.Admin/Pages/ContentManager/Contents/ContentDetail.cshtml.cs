using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;

namespace Oryx.Content.Admin.Pages.ContentManager.Contents
{
    public class ContentDetailModel : PageModel
    {
        private ContentBusiness contentBusiness;
        public ContentDetailModel(ContentBusiness _bannerBusiness)
        {
            contentBusiness = _bannerBusiness;
        }

        public List<Model.ContentEntry> ContentEntryList = new List<Model.ContentEntry>();

        public string CategoryName { get; set; }

        public async Task OnGet(Guid pid, int skipCount = 0, int pageSize = 20)
        {
            var category = await contentBusiness.GetCategoryById(pid);
            CategoryName = category?.Category?.Name;
            var result = await contentBusiness.GetContentListByCategoryId(pid);
            ContentEntryList = result.OrderBy(x => x.Order).ToList();
        }
    }
}