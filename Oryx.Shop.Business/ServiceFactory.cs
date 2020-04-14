using Microsoft.Extensions.DependencyInjection;
using Oryx.Shop.Business.Bs.Goods;
using Oryx.Shop.Business.Bs.Order;
using Oryx.Shop.Business.Bs.PayLog;
using Oryx.Shop.Business.Bs.ShoppingBascket;

namespace Oryx.Shop.Business
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddShopBusiness(this IServiceCollection services)
        {
            services 
                .AddTransient<GoodsBs>()
                .AddTransient<OrderBs>()
                .AddTransient<ShoppingBascketBs>()
                .AddTransient<PayLogBs>();
            return services;
        }
    }
}
