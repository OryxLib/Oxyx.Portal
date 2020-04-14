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

namespace Oryx.SNS.Web.Filters
{
    public class PageRoleAuthentication : TypeFilterAttribute
    {
        public PageRoleAuthentication(params string[] _targetRoleList) : base(typeof(PageRoleAuthenticationImp))
        {
            Arguments = new object[] { _targetRoleList }; ;
        }
        private class PageRoleAuthenticationImp : Attribute, IAsyncPageFilter
        {
            private readonly IDistributedCache distributedCache;
            private readonly UserAccountBusiness userAccountBusiness;
            private readonly string[] targetRoleList;
            public PageRoleAuthenticationImp(IDistributedCache _distributedCache,
                UserAccountBusiness _userAccountBusiness,
                string[] _targetRoles)
            {
                distributedCache = _distributedCache;
                userAccountBusiness = _userAccountBusiness;
                targetRoleList = _targetRoles;
            }

            public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
            {
                try
                {
                    await context.HttpContext.Session.LoadAsync();
                    byte[] storeData;
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
                                var roles = await userAccountBusiness.GetRoles(strValue);
                                if (roles.Any(x => targetRoleList.Any(c => c == x)))
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
}
