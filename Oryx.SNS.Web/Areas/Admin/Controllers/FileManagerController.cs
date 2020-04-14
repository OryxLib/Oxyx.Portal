using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.CommonDbOperation;
using Oryx.Content.DataAccessor;
using Oryx.Content.Model;
using Oryx.SNS.Web.BaseControllers;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class FileManagerController : BaseController<FileEntry>
    {
        public FileManagerController(DataFileEntryAccessor _accessor) : base(_accessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}