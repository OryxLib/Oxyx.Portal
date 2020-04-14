using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;

namespace Oryx.Social.DataAccessor
{
    public class PostEntryFileDataAccessor : CommonAccessor
    {
        public PostEntryFileDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
