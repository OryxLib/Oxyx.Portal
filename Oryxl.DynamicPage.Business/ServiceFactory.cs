using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryxl.DynamicPage.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDynamicBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<DynamicPageBusiness>()
                .AddTransient<FormEntryBusiness>();
            return services;
        }
    }
}
