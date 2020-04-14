using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Portal.Filters;

namespace Oryx.Content.Portal.Pages.Social
{
    [TypeFilter(typeof(PageAuthentication))]
    public class IndexModel : PageModel
    {
        public string UserId { get; set; }

        public async Task OnGet()
        {
            await HttpContext.Session.LoadAsync();
            UserId = HttpContext.Session.GetString("UserId");
        }
    }
}