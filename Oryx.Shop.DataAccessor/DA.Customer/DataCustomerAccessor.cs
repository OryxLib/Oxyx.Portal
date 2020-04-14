using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using Oryx.Shop.Model;

namespace Oryx.Shop.DataAccessor.DA.Customer
{
    public class DataCustomerAccessor : CommonAccessor
    {
        public DataCustomerAccessor(CommonDbContext _ShopDbContext, IDistributedCache _DistributedCache) :
            base(_ShopDbContext, _DistributedCache)
        {
        }
    }
}
