using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Oryx.DataPlatForm.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDataPlatformBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<DataOperation>();
            return services;
        }
    }
}
