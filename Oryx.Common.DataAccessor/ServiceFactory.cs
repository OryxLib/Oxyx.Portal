using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Common.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddSandBoxDataAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<FeedbackAccessor>()
                .AddTransient<TipEntryDataAccessor>()
                .AddTransient<SandBoxDataAccessor>();
            return services;
        }
    }
}
