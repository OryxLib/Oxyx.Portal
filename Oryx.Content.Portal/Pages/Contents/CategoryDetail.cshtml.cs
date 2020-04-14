using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;
using Oryx.Content.Model;

namespace Oryx.Content.Portal.Pages.Contents
{
    public class CategoryDetailModel : PageModel
    {
        ContentBusiness contentBusiness;
        public Categories Categories { get; set; }
        public List<CategoryComment> CategoryComments { get; set; }
        public CategoryDetailModel(ContentBusiness _contentBusiness)
        {
            contentBusiness = _contentBusiness;
        }

        public async Task OnGet(Guid? cid)
        {
            if (cid != null)
            {
                var categoryResult = await contentBusiness.GetCategoryWithDetailById(cid.Value);
                Categories = categoryResult.Category;
                CategoryComments = categoryResult.CategoryComments;
            }
            else
            {
                await contentBusiness.GetTopCategory("");
            }
        }
    }
}