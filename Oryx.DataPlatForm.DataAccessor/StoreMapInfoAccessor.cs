using Oryx.CommonDbOperation;
using System;

namespace Oryx.DataPlatForm.DataAccessor
{
    public class StoreMapInfoAccessor : CommonAccessor
    {
        public StoreMapInfoAccessor(CommonDbContext _ShopDbContext,
            Microsoft.Extensions.Caching.Distributed.IDistributedCache _DistributedCache)
            : base(_ShopDbContext, _DistributedCache)
        {

        }
    }
}
