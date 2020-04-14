using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SNS.Business
{
    public class SNSDataAccessor : CommonAccessor
    {
        public SNSDataAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) 
            : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
