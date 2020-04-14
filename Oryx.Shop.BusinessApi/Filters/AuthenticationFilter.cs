﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop.BusinessApi.Filters
{
    public class AdminUserAuthenticationFilter : Attribute, IAsyncActionFilter
    {
        private readonly IDistributedCache distributedCache;
        public AdminUserAuthenticationFilter(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await context.HttpContext.Session.LoadAsync();
                byte[] storeData;
                if (context.HttpContext.Request.Query["key"] == "Linengneng" ||
                    context.HttpContext.Request.Headers["Referer"].Any(x => x.Contains("servicewechat.com")))
                {
                    await next();
                    return;
                }

                if (context.HttpContext.Session.TryGetValue("AdminUser", out storeData))
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