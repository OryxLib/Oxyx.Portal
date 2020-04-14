using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Shop.Filters;
using Oryx.Shop.Model.Shop.Goods;

namespace Oryx.Shop.Pages.Good
{
    public class GoodModel : PageModel
    {
        public GoodEntity good;

        [TypeFilter(typeof(PageAuthenticationFilter))]
        public void OnGet(string id = "")
        {
            if (id != "")
            {//修改

            }
        }
    }
}