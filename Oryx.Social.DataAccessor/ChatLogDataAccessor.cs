using Oryx.CommonDbOperation;
using System;

namespace Oryx.Social.DataAccessor
{
    public class ChatLogDataAccessor : CommonAccessor
    {
        public ChatLogDataAccessor(CommonDbContext _ShopDbContext, Microsoft.Extensions.Caching.Distributed.IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
