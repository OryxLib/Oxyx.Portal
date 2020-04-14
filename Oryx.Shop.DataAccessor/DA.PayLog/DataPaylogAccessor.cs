using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;

namespace Oryx.Shop.DataAccessor.DA.PayLog
{
    public class DataPaylogAccessor : CommonAccessor
    {
        public DataPaylogAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
