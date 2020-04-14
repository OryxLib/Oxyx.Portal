using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Content.DataAccessor
{
    public class DataContentEntryAccessor : CommonAccessor
    {
        public DataContentEntryAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }

     
    }
}
