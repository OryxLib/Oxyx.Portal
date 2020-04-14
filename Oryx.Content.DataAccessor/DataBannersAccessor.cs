using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.DataAccessor
{
    public class DataBannersAccessor : CommonAccessor
    {
        public DataBannersAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
