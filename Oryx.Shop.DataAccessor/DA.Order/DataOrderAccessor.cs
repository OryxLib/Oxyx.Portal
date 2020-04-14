using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;

namespace Oryx.Shop.DataAccessor.DA.Order
{
    public class DataOrderAccessor : CommonAccessor
    {
        public DataOrderAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
