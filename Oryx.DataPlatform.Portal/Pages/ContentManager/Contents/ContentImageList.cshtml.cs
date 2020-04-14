using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;

namespace Oryx.DataPlatform.Portal.Pages.ContentManager.Contents
{
    public class ContentImageListModel : PageModel
    {
        private ContentBusiness contentBusiness;
        public ContentImageListModel(ContentBusiness _contentBusiness)
        {
            contentBusiness = _contentBusiness;
        }

        public List<string> ImgList = new List<string>();

        public async Task OnGet(Guid id)
        {
            var content = await contentBusiness.GetContentById(id);
            ImgList = content.MediaResource.Select(x => x.ActualPath).ToList();
        }
    }
}