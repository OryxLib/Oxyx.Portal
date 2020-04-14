using Oryx.CommonDbOperation;
using System;

namespace Oryx.UserActivity.DataAccessor
{
    public class DataActivityAccessor : CommonAccessor
    {
        public DataActivityAccessor(CommonDbContext _ShopDbContext, Microsoft.Extensions.Caching.Distributed.IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
