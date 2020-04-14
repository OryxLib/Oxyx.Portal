using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryxl.DynamicPage.Accessor
{
    public class RouteAccessor : CommonAccessor
    {
        public RouteAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
