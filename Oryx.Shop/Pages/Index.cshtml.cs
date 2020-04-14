using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Shop;

namespace Oryx.Shop.Pages
{

    [TypeFilter(typeof(PageAuthenticationFilter))]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}