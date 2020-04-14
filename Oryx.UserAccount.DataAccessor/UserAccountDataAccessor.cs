using Oryx.CommonDbOperation;
using System;

namespace Oryx.UserAccount.DataAccessor
{
    public class UserAccountDataAccessor : CommonAccessor
    {
        public UserAccountDataAccessor(CommonDbContext _ShopDbContext, Microsoft.Extensions.Caching.Distributed.IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        } 
    }
}
