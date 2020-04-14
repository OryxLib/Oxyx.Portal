using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;

namespace Oryx.Shop.DataAccessor.DA.ShoppingBascket
{
    public class DataShoppingBascketAccessor : CommonAccessor
    {
        public DataShoppingBascketAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) : base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
