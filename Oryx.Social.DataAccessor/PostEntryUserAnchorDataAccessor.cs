using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;

namespace Oryx.Social.DataAccessor
{
    public class PostEntryUserAnchorDataAccessor : CommonAccessor
    {
        public PostEntryUserAnchorDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
