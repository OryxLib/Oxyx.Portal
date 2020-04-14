using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Shop.Business.Bs.Goods;
using Oryx.Shop.Model.Shop.Goods;

namespace Oryx.Shop.Pages.Good
{
    public class ListModel : PageModel
    {

        public ListModel(GoodsBs _goodsBs)
        {
        }

        public void OnGet()
        {
        }
    }
}