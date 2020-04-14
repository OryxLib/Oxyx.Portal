using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.UserAccount.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddUserAccountDataAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<UserAccountDataAccessor>();
            return services;
        }
    }
}
