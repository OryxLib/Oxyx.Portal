using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.DynamicPage.Middleware
{
    public class BaseController<T> : Controller
        where T : class, new()
    {
        //public async Task<IActionResult> List()
        //{

        //}
    }
}
