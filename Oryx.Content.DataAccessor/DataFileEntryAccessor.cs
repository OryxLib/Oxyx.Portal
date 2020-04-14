using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.DataAccessor
{
    public class DataFileEntryAccessor : CommonAccessor
    {
        public DataFileEntryAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) :
            base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
