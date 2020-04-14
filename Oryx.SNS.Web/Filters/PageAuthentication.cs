using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.SNS.Web.Filters.Attributes;
using Oryx.UserAccount.Business;
using Oryx.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.SNS.Web.Filters
{
    public class PageAuthentication : IAsyncPageFilter
    {
        private readonly IDistributedCache distributedCache;
        public PageAuthentication(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            try
            {
                await context.HttpContext.Session.LoadAsync();
                byte[] storeData;
                var type = context.HandlerInstance.GetType();
                var hasIgnore = type.CustomAttributes.Any(x => x.AttributeType == typeof(PageIgnoreAttribute));
                if (hasIgnore)
                {
                    await next();
                    return;
                }
                var loginPage = context.HttpContext.Request.Path.Value.Contains("/Account/Login");
                if (loginPage)
                {
                    await next();
                }
                else
                {
                    if (context.HttpContext.Session.TryGetValue(UserAccountBusiness.UserAccountSessionkey, out storeData))
                    {
                        var strValue = Encoding.UTF8.GetString(storeData);
                        if (!string.IsNullOrEmpty(strValue))
                        {
                            await next();
                        }
                        else
                        {
                            context.HttpContext.Response.Redirect("/Account/Login?return_url=" + context.HttpContext.Request.Path);
                        }
                    }
                    else
                    {
                        context.HttpContext.Response.Redirect("/Account/Login?return_url=" + context.HttpContext.Request.Path);
                    }
                }
            }
            catch (Exception exc)
            {
                context.HttpContext.Response.Redirect("/Account/Login?return_url=" + context.HttpContext.Request.Path);
            }
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }
    }
}
