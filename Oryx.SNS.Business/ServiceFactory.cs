using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SNS.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddSNSBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<SNSDataAccessor>() 
                .AddTransient<SNSBusiness>();
            return services;
        }
    }
}
