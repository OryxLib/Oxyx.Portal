using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.UserAccount.Business;
using Oryx.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Content.Portal.Filters
{
    public class AccountUserAuthenticationFilter : Attribute, IAsyncActionFilter
    {
        private readonly IDistributedCache distributedCache;
        public AccountUserAuthenticationFilter(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await context.HttpContext.Session.LoadAsync();
                byte[] storeData;
                if (context.HttpContext.Session.TryGetValue(UserAccountBusiness.UserAccountSessionkey, out storeData))
                {
                    var strValue = Encoding.UTF8.GetString(storeData);
                    if (strValue == "true")
                    {
                        await next();
                    }
                    else
                    {
                        context.Result = new JsonResult(new ApiMessage
                        {
                            Success = false,
                            Message = "Permission denied"
                        });
                    }
                }
                else
                {
                    context.Result = new JsonResult(new ApiMessage
                    {
                        Success = false,
                        Message = "Permission denied"
                    });
                }
            }
            catch (Exception exc)
            {
                context.Result = new JsonResult(new ApiMessage
                {
                    Success = false,
                    Message = exc.Message
                });
            }
        }
    }
}
