using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;

namespace Oryx.Social.DataAccessor
{
    public class PostEntryCommentsDataAccessor : CommonAccessor
    {
        public PostEntryCommentsDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
