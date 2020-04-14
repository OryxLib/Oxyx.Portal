using Microsoft.Extensions.DependencyInjection; 
using Oryx.Shop.DataAccessor.DA.Goods;
using Oryx.Shop.DataAccessor.DA.Order;
using Oryx.Shop.DataAccessor.DA.PayLog;
using Oryx.Shop.DataAccessor.DA.ShoppingBascket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.DataAccessor
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddShopDataAccessor(this IServiceCollection services)
        {
            services 
                .AddTransient<DataGoodsAccessor>()
                .AddTransient<DataOrderAccessor>()
                .AddTransient<DataPaylogAccessor>()
                .AddTransient<DataPaylogAccessor>();
            return services;
        }
    }
}