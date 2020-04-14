using Microsoft.Extensions.DependencyInjection;

namespace Oryx.DataPlatForm.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddDataPlatformAccessor(this IServiceCollection services)
        {
            services
                .AddTransient<StoreMapInfoAccessor>();
            return services;
        }

    }
}
