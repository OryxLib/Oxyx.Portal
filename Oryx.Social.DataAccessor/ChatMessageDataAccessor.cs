using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;

namespace Oryx.Social.DataAccessor
{
    public class ChatMessageDataAccessor : CommonAccessor
    {
        public ChatMessageDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
