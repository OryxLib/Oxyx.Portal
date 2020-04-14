using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Oryx.CommonDbOperation;
using Oryxl.DynamicPage.Model;
using System;
using System.Linq;
using Oryxl.DynamicPage.Model;
using System.Collections.Generic;
using Oryxl.DynamicPage.Business;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Oryx.DynamicPage
{
    public static class DynamicPageMiddleware
    {
        static string defaultRoute = "index";
        private static DynamicPageBusiness dynamicPageBusiness;
        public static IApplicationBuilder UseDatabasePageRedner(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
           {
               var route = ctx.Request.Path;
               dynamicPageBusiness = ctx.RequestServices.GetService<DynamicPageBusiness>();
               var pageString = await GenericPage(route);
               if (!string.IsNullOrEmpty(pageString))
               {
                   var strBuffer = Encoding.UTF8.GetBytes(pageString);
                   await ctx.Response.Body.WriteAsync(strBuffer, 0, strBuffer.Length);
                   ctx.Response.ContentType = "text/html";
                   ctx.Response.StatusCode = 200;
               }
               else
               {
                   await next();
               }
           });
            return app;
        }


        public static async Task<string> GenericPage(string route)
        {
            if (route == "" || route == "/")
            {
                route = defaultRoute;
            }

            return await dynamicPageBusiness.GetContentByRoute(route);
        }



        private static bool checkDefaultRoute(string route)
        {
            if (route.ToLower() == defaultRoute)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
