using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.UserAccount.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddUserAccountBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<UserAccountBusiness>();
            return services;
        }
    }
}
