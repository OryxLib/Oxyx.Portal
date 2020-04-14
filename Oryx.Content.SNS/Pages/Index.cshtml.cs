using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;
using Oryx.Content.Model;

namespace Oryx.Content.Portal.Pages
{
    public class IndexModel : PageModel
    {
        public ContentBusiness contentBusiness { get; set; }
        public IndexModel(ContentBusiness _contentBusiness)
        {
            contentBusiness = _contentBusiness;
        }
        public IList<Categories> Ctegories { get; set; }
        public int TotalNum { get; set; }
        public int currentIndex = 1;
        public async Task OnGet(string query = "", int pageIndex = 1, int pageSize = 12)
        {
            currentIndex = pageIndex;
            var record = await contentBusiness.GetTopSexHotWithTotalNum( (pageIndex - 1) * pageSize, pageSize);
            Ctegories = record.Item1;
            TotalNum = record.Item2;
        }
    }
}
