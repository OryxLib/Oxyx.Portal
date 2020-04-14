using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryxl.DynamicPage.Accessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDynamicPageAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<FormEntryAccessor>()
                .AddTransient<PageAccessor>() 
                .AddTransient<RouteAccessor>();
            return services;
        }
    }
}
