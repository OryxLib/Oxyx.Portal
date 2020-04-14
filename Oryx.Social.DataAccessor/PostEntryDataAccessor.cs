using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oryx.Social.DataAccessor
{
    public class PostEntryDataAccessor : CommonAccessor
    {
        public PostEntryDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        } 
    }
}
