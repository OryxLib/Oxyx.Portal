using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.CommonDbOperation;
using Oryx.SNS.Web.BaseControllers;
using Oryx.UserAccount.DataAccessor;
using Oryx.UserAccount.Model;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : BaseController<UserAccountEntry>
    {
        public AccountController(UserAccountDataAccessor _accessor) : base(_accessor)
        {
        }


    }
}