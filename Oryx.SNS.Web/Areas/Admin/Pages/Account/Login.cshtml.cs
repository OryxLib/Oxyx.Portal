using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Oryx.SNS.Web.Admin.Pages.Account
{
    public class LoginModel : PageModel
    {
        public string ReturnUrl = string.Empty;

        public void OnGet()
        {
            if (Request.Query.ContainsKey("return_url"))
            {
                ReturnUrl = Request.Query["return_url"];
            }
        }
    }
}