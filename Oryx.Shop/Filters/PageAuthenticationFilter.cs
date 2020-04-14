using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop
{
    public class PageAuthenticationFilter : IAsyncPageFilter
    {
        private readonly IDistributedCache distributedCache;
        public PageAuthenticationFilter(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            try
            {
                await context.HttpContext.Session.LoadAsync();
                byte[] storeData;
                if (context.HttpContext.Session.TryGetValue("AdminUser", out storeData))
                {
                    var strValue = Encoding.UTF8.GetString(storeData);
                    if (strValue == "true")
                    {
                        await next();
                    }
                    else
                    {
                        context.HttpContext.Response.Redirect("/login");
                    }
                }
                else
                {
                    context.HttpContext.Response.Redirect("/login");
                }
            }
            catch (Exception exc)
            {
                context.HttpContext.Response.Redirect("/login");
            }
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }
    }
}
