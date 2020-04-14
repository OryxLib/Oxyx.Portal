using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryxl.DynamicPage.Accessor
{
    public class PageAccessor : CommonAccessor
    {
        public PageAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
