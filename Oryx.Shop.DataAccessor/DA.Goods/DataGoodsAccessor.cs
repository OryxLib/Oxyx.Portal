using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;

namespace Oryx.Shop.DataAccessor.DA.Goods
{
    public class DataGoodsAccessor : CommonAccessor
    {
        public DataGoodsAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) :
            base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
