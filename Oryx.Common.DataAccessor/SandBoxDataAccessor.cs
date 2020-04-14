using Oryx.CommonDbOperation;
using System;

namespace Oryx.Common.DataAccessor
{
    public class SandBoxDataAccessor : CommonAccessor
    {
        public SandBoxDataAccessor(CommonDbContext _ShopDbContext, Microsoft.Extensions.Caching.Distributed.IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
