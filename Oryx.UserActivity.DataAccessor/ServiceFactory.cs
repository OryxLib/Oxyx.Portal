using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.UserActivity.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddActivityDataAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<DataActivityAccessor>();
            return services;
        }
    }
}
