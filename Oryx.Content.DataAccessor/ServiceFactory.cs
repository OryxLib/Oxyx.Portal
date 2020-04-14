using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddContentDataAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<DataCategoriesAccessor>()
                .AddTransient<DataCommentAccessor>()
                .AddTransient<DataContentEntryAccessor>()
                .AddTransient<DataContentPayLogAccessor>()
                .AddTransient<DataFileEntryAccessor>()
                .AddTransient<DataBannersAccessor>();
            return services;
        }
    }
}
