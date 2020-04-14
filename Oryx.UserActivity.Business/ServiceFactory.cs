using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.UserActivity.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddActivityBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<ActivityBusiness>();
            return services;
        }
    }
}
