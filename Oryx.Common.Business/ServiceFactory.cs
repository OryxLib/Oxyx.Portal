using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Common.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddSandBoxDataBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<TipEntryBussiness>()
                .AddTransient<FeedbackBusiness>()
                .AddTransient<SandBoxBusiness>();
            return services;
        }
    }
}
