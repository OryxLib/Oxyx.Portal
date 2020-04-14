using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oryx.Content.Admin.Controllers
{
    public class ReportManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}