using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddContentBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<KnowledgeBusiness>()
                .AddTransient<FaqBusiness>()
                .AddTransient<FileEntryBusiness>()
                .AddTransient<ContentBusiness>()
                .AddTransient<CategoryOutput>()
                .AddTransient<BannersBusienss>();
            return services;
        }
    }
}
